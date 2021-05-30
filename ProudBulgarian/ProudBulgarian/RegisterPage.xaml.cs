using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using ProudBulgarian.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProudBulgarian
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void registerButton_Clicked(object sender, EventArgs e)
        {
            
            if(passwordEntry.Text == confimPasswordEntry.Text)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {

                    if (conn.Table<User>().ToList().Where(u => u.Email == emailEntry.Text || u.Name == usernameEntry.Text).FirstOrDefault() != null)
                    {
                        DisplayAlert("Error", "Email or Username is already used", "Ok");
                    } else
                    {
                        int rows = 0;
                        User user = new User()
                        {
                            Name = usernameEntry.Text,
                            Email = emailEntry.Text,
                            Password = passwordEntry.Text,
                            Duels_Won = 0,
                            Duels_Played = 0,
                            Single_Player_Progress = 0,
                            Level = 1,
                            Experience = 0
                        };


                        conn.CreateTable<User>();
                        rows = conn.Insert(user);
                        if (rows > 0)
                        {
                            App.Username = user.Name;
                            DisplayAlert("Success", "Successful registration", "Ok");
                            Navigation.PushAsync(new HomePage());
                        }
                        else
                        {
                            DisplayAlert("Error", "Unsuccessful registration", "Ok");
                        }
                    }
                }
            }
            else
            {
                DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }
    }
}