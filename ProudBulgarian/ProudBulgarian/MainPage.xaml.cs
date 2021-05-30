using ProudBulgarian.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProudBulgarian
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource(
                "ProudBulgarian.Assets.Images.gerb.png", 
                assembly);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            Question question0 = new Question()
            {
                Text = "When was Bulgaria founded?",
                Right_Answer = "681",
                First_Answer = "893",
                Second_Answer ="632",
                Third_Answer = "681",
                Fourth_Answer = "716"
            };

            Question question1 = new Question()
            {
                Text = "Who was Bulgaria's first ruler?",
                Right_Answer = "Asparuh",
                First_Answer = "Kubrat",
                Second_Answer = "Asparuh",
                Third_Answer = "Kotrag",
                Fourth_Answer = "Kuber"
            };

            Question question2 = new Question()
            {
                Text = "What was Bulgaria's first capital?",
                Right_Answer = "Pliska",
                First_Answer = "Preslav",
                Second_Answer = "Sofia",
                Third_Answer = "Veliko Tarnovo",
                Fourth_Answer = "Pliska"
            };

            Question question3 = new Question()
            {
                Text = "When was the Christianization of Bulgaria?",
                Right_Answer = "864",
                First_Answer = "864",
                Second_Answer = "886",
                Third_Answer = "870",
                Fourth_Answer = "832"
            };

            Question question4 = new Question()
            {
                Text = "Who was the ruler who Christianized the nation?",
                Right_Answer = "Boris I",
                First_Answer = "Simeon I",
                Second_Answer = "Boris I",
                Third_Answer = "Krum",
                Fourth_Answer = "Asparuh"
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Question>();
                conn.Insert(question1);
                conn.Insert(question2);
                conn.Insert(question3);
                conn.Insert(question4);
                conn.Insert(question0);
            }


            var isUsernameEmpty = string.IsNullOrEmpty(usernameEntry.Text);
            var isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                if (isPasswordEmpty || isUsernameEmpty)
                {
                    // empty fields
                }
                else if(conn.Table<User>().ToList()
                    .FirstOrDefault(u => u.Name == usernameEntry.Text && u.Password == passwordEntry.Text) == null)
                {
                    //not found
                }
                else
                {
                    App.Username = usernameEntry.Text;
                    Navigation.PushAsync(new HomePage());
                }
            }
        }

        private void registerUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
