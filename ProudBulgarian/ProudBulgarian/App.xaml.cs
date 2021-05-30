using Microsoft.WindowsAzure.MobileServices;
using ProudBulgarian.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProudBulgarian
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static string Username = string.Empty;
        public static int? CurrentChallengeId = null;
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
