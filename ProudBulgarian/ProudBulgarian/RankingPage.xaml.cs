using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProudBulgarian.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;

namespace ProudBulgarian
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RankingPage : ContentPage
    {
        public static MobileServiceClient client = new MobileServiceClient("https://proudbulgarianapp.azurewebsites.net");
        public RankingPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();//TODO without curr player
                userListView.ItemsSource = users;
            }


        }

        private void Challenge_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            StackLayout listViewItem = (StackLayout)button.Parent;
            Label label = (Label)listViewItem.Children[0];

            String name = label.Text;//name
            //get random questions
            var questions = RandomQuestions(5);
            Challenge challenge = new Challenge()
            {
                Challenger_Name = App.Username,
                Defender_Name = name,
                First_Question = questions[0],
                Second_Question = questions[1],
                Third_Question = questions[2],
                Fourth_Question = questions[3],
                Fifth_Question = questions[4],
                Challenger_Finished_Questions = 0,
                Defender_Finished_Questions = 0,
                Answered_Right_Challenger_Questions = 0,
                Answered_Right_Defender_Questions = 0,
                Challenger_Finished = false,
                Defender_Finished = false,
                Defender_Started = false
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                conn.Insert(challenge);
                var challenge1 = conn.Table<Challenge>().ToList().Where(q => 
                q.Challenger_Name == challenge.Challenger_Name &&
                q.Defender_Name == challenge.Defender_Name &&
                q.First_Question == challenge.First_Question && 
                q.Second_Question == challenge.Second_Question &&
                q.Third_Question == challenge.Third_Question &&
                q.Fourth_Question == challenge.Fourth_Question &&
                q.Fifth_Question == challenge.Fifth_Question &&
                q.Challenger_Finished_Questions == challenge.Challenger_Finished_Questions &&
                q.Defender_Finished_Questions == challenge.Defender_Finished_Questions &&
                q.Answered_Right_Challenger_Questions == challenge.Answered_Right_Challenger_Questions &&
                q.Answered_Right_Defender_Questions == challenge.Answered_Right_Defender_Questions
                ).FirstOrDefault();
                App.CurrentChallengeId = challenge1.Id;
            }
            
            Navigation.PushAsync(new QuestionPage());
        }

        private int[] RandomQuestions(int numberOfQuestions)
        {
            int[] questionsIds = new int[numberOfQuestions];
            int counter = 0;
            var rand = new Random();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Question>();
                while (counter < numberOfQuestions)
                {
                    int currNumber = rand.Next(conn.Table<Question>().ToList().Count() + 1);
                    if (conn.Table<Question>().ToList().Where(q => q.Id == currNumber).FirstOrDefault() != null)
                    {
                        if (!questionsIds.Contains(currNumber))
                        {
                            questionsIds[counter] = currNumber;
                            counter++;
                        }
                    }
                }
            }
            return questionsIds;
        }
    }
}