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
    public partial class QuestionPage : ContentPage
    {
        public string answer = "";
        public QuestionPage()
        {
            InitializeComponent();
            
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                var challenge = conn.Table<Challenge>().ToList().Where(c => c.Id == App.CurrentChallengeId).First();
                int questionId;
                int currQuestion = challenge.Challenger_Finished ? challenge.Defender_Finished_Questions : challenge.Challenger_Finished_Questions;
                switch (currQuestion) {
                    case 0:
                        questionId = challenge.First_Question;
                        break;
                    case 1:
                        questionId = challenge.Second_Question;
                        break;
                    case 2:
                        questionId = challenge.Third_Question;
                        break;
                    case 3:
                        questionId = challenge.Fourth_Question;
                        break;
                    case 4:
                        questionId = challenge.Fifth_Question;
                        break;
                    default:
                        questionId = -1;
                        break;
                }

                conn.CreateTable<Question>();
                var question = conn.Table<Question>().ToList().Where(q => q.Id == questionId+1).FirstOrDefault();  
                questionLabel.Text = question.Text;
                firstAnswer.Text = question.First_Answer;
                secondAnswer.Text = question.Second_Answer;
                thirdAnswer.Text = question.Third_Answer;
                fourthAnswer.Text = question.Fourth_Answer;
                answer = question.Right_Answer;
            }
        }

        private async void firstAnswer_Clicked(object sender, EventArgs e)
        {
            Challenge challenge;
            ColorRightAnswer();
            await Task.Delay(1000);
            firstAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            secondAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            thirdAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            fourthAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                challenge = conn.Table<Challenge>().ToList().Where(c => c.Id == App.CurrentChallengeId).FirstOrDefault();
                if (answer == firstAnswer.Text)
                {
                    challenge.RightAnswer();
                }
                challenge.FinishQuestion();
                conn.Update(challenge);
                if (challenge.Defender_Finished && challenge.Challenger_Finished)
                {
                    if (challenge.Answered_Right_Challenger_Questions > challenge.Answered_Right_Defender_Questions)
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 50;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 10;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                            String.Format("You lost the challenge {0} to {1} right answers. Gained 10 Experience",
                                challenge.Answered_Right_Defender_Questions,
                                challenge.Answered_Right_Challenger_Questions),
                            "Ok");
                    }
                    else if (challenge.Answered_Right_Challenger_Questions < challenge.Answered_Right_Defender_Questions)
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 10;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 50;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                             String.Format("You won the challenge {0} to {1} right answers. Gained 50 Experience",
                                 challenge.Answered_Right_Defender_Questions,
                                 challenge.Answered_Right_Challenger_Questions),
                             "Ok");
                    }
                    else
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 30;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 30;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                            String.Format("You drew the challenge {0} to {1} right answers. Gained 30 Experience",
                                challenge.Answered_Right_Defender_Questions,
                                challenge.Answered_Right_Challenger_Questions),
                            "Ok");
                    }

                    conn.Delete(challenge);
                }
            }
            if(!(challenge.Challenger_Finished && !challenge.Defender_Started) && !(challenge.Defender_Started && challenge.Defender_Finished)) {
                await Navigation.PushAsync(new QuestionPage());
            } else
            {
                App.CurrentChallengeId = null;
                await Navigation.PushAsync(new HomePage());
            }

        }

        private async void secondAnswer_Clicked(object sender, EventArgs e)
        {
            Challenge challenge;
            ColorRightAnswer();
            await Task.Delay(1000);
            firstAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            secondAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            thirdAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            fourthAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                challenge = conn.Table<Challenge>().ToList().Where(c => c.Id == App.CurrentChallengeId).FirstOrDefault();
                if (answer == secondAnswer.Text)
                {
                    challenge.RightAnswer();
                }
                challenge.FinishQuestion();
                conn.Update(challenge);
                if (challenge.Defender_Finished && challenge.Challenger_Finished)
                {
                    if (challenge.Answered_Right_Challenger_Questions > challenge.Answered_Right_Defender_Questions)
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 50;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 10;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                            String.Format("You lost the challenge {0} to {1} right answers. Gained 10 Experience",
                                challenge.Answered_Right_Defender_Questions,
                                challenge.Answered_Right_Challenger_Questions),
                            "Ok");
                    }
                    else if (challenge.Answered_Right_Challenger_Questions < challenge.Answered_Right_Defender_Questions)
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 10;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 50;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                             String.Format("You won the challenge {0} to {1} right answers. Gained 50 Experience",
                                 challenge.Answered_Right_Defender_Questions,
                                 challenge.Answered_Right_Challenger_Questions),
                             "Ok");
                    }
                    else
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 30;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 30;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                            String.Format("You drew the challenge {0} to {1} right answers. Gained 30 Experience",
                                challenge.Answered_Right_Defender_Questions,
                                challenge.Answered_Right_Challenger_Questions),
                            "Ok");
                    }

                    conn.Delete(challenge);
                }
            }
            if (!(challenge.Challenger_Finished && !challenge.Defender_Started) && !(challenge.Defender_Started && challenge.Defender_Finished))
            {
                await Navigation.PushAsync(new QuestionPage());
            } else
            {
                App.CurrentChallengeId = null;
                await Navigation.PushAsync(new HomePage());
            }
        }

        private async void thirdAnswer_Clicked(object sender, EventArgs e)
        {
            Challenge challenge;
            ColorRightAnswer();
            await Task.Delay(1000);
            firstAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            secondAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            thirdAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            fourthAnswer.BackgroundColor = Color.FromHex("#1E90FF");

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                challenge = conn.Table<Challenge>().ToList().Where(c => c.Id == App.CurrentChallengeId).FirstOrDefault();
                if (answer == thirdAnswer.Text)
                {
                    challenge.RightAnswer();
                }
                challenge.FinishQuestion();
                conn.Update(challenge);
                if(challenge.Defender_Finished && challenge.Challenger_Finished)
                {
                    if(challenge.Answered_Right_Challenger_Questions > challenge.Answered_Right_Defender_Questions)
                    {                      
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 50;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 10;
                        conn.Update(defender);
                        await DisplayAlert("Result!", 
                            String.Format("You lost the challenge {0} to {1} right answers. Gained 10 Experience",
                                challenge.Answered_Right_Defender_Questions, 
                                challenge.Answered_Right_Challenger_Questions), 
                            "Ok");
                    } else if (challenge.Answered_Right_Challenger_Questions < challenge.Answered_Right_Defender_Questions)
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 10;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 50;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                             String.Format("You won the challenge {0} to {1} right answers. Gained 50 Experience",
                                 challenge.Answered_Right_Defender_Questions,
                                 challenge.Answered_Right_Challenger_Questions),
                             "Ok");
                    } else
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 30;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 30;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                            String.Format("You drew the challenge {0} to {1} right answers. Gained 30 Experience",
                                challenge.Answered_Right_Defender_Questions,
                                challenge.Answered_Right_Challenger_Questions),
                            "Ok");
                    }

                    conn.Delete(challenge);
                }
            }
            if (!(challenge.Challenger_Finished && !challenge.Defender_Started) && !(challenge.Defender_Started && challenge.Defender_Finished))
            {
                await Navigation.PushAsync(new QuestionPage());
            } else
            {
                App.CurrentChallengeId = null;
                await Navigation.PushAsync(new HomePage());
            }
        }

        private async void fourthAnswer_Clicked(object sender, EventArgs e)
        {
            Challenge challenge;
            ColorRightAnswer();
            await Task.Delay(1000);
            firstAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            secondAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            thirdAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            fourthAnswer.BackgroundColor = Color.FromHex("#1E90FF");
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Challenge>();
                challenge = conn.Table<Challenge>().ToList().Where(c => c.Id == App.CurrentChallengeId).FirstOrDefault();
                if (answer == fourthAnswer.Text)
                {
                    challenge.RightAnswer();
                }
                challenge.FinishQuestion();
                conn.Update(challenge);
                if (challenge.Defender_Finished && challenge.Challenger_Finished)
                {
                    if (challenge.Answered_Right_Challenger_Questions > challenge.Answered_Right_Defender_Questions)
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 50;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 10;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                            String.Format("You lost the challenge {0} to {1} right answers. Gained 10 Experience",
                                challenge.Answered_Right_Defender_Questions,
                                challenge.Answered_Right_Challenger_Questions),
                            "Ok");
                    }
                    else if (challenge.Answered_Right_Challenger_Questions < challenge.Answered_Right_Defender_Questions)
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 10;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 50;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                             String.Format("You won the challenge {0} to {1} right answers. Gained 50 Experience",
                                 challenge.Answered_Right_Defender_Questions,
                                 challenge.Answered_Right_Challenger_Questions),
                             "Ok");
                    }
                    else
                    {
                        var challenger = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Challenger_Name).FirstOrDefault();
                        challenger.Experience += 30;
                        conn.Update(challenger);
                        var defender = conn.Table<User>().ToList()
                            .Where(c => c.Name == challenge.Defender_Name).FirstOrDefault();
                        defender.Experience += 30;
                        conn.Update(defender);
                        await DisplayAlert("Result!",
                            String.Format("You drew the challenge {0} to {1} right answers. Gained 30 Experience",
                                challenge.Answered_Right_Defender_Questions,
                                challenge.Answered_Right_Challenger_Questions),
                            "Ok");
                    }

                    conn.Delete(challenge);
                }
            }
            if (!(challenge.Challenger_Finished && !challenge.Defender_Started) && !(challenge.Defender_Started && challenge.Defender_Finished))
            {
                await Navigation.PushAsync(new QuestionPage());
            } else
            {
                App.CurrentChallengeId = null;
                await Navigation.PushAsync(new HomePage());
            }
        }

        private void ColorRightAnswer()
        {
            if (firstAnswer.Text == answer)
            {
                firstAnswer.BackgroundColor = Color.Green;
            }
            else if (secondAnswer.Text == answer)
            {
                secondAnswer.BackgroundColor = Color.Green;
            }
            else if (thirdAnswer.Text == answer)
            {
                thirdAnswer.BackgroundColor = Color.Green;
            }
            else if (fourthAnswer.Text == answer)
            {
                fourthAnswer.BackgroundColor = Color.Green;
            }
        }
    }
}