using Microsoft.WindowsAzure.MobileServices;
using ProudBulgarian.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProudBulgarian
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var user = conn.Table<User>().ToList().FirstOrDefault(u => u.Name == App.Username);
                userProfileView.ItemsSource = new List<User> { user };
            }
        }
    }
}