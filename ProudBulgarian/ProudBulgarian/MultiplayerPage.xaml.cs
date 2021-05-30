using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProudBulgarian.Model;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProudBulgarian
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiplayerPage : ContentPage
    {
        public MultiplayerPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                var challenges = conn.Table<Challenge>().ToList()
                    .Where(c => c.Defender_Name == App.Username);
                challengersListView.ItemsSource = challenges;
            }
        }

        private void AcceptChallenge_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            StackLayout listViewItem = (StackLayout)button.Parent;
            Label label = (Label)listViewItem.Children[0];
            String name = label.Text;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                var acceptedChallenge = conn.Table<Challenge>().ToList()
                    .Where(c => c.Challenger_Name == name).FirstOrDefault();
                acceptedChallenge.Defender_Started = true;
                conn.Update(acceptedChallenge);
                App.CurrentChallengeId = acceptedChallenge.Id;
            }
            Navigation.PushAsync(new QuestionPage());
        }
    }
}