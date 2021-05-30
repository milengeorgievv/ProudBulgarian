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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void Singleplayer_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingleplayerPage());
        }

        private void Challenges_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MultiplayerPage());
        }

        private void Challenge_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DailyChallengePage());
        }

        private void Ranking_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RankingPage());
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            //quit the app
        }
    }
}