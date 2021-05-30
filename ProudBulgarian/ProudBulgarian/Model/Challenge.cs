using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProudBulgarian.Model
{
    public class Challenge
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Challenger_Name { get; set; }
        [MaxLength(50)]
        public string Defender_Name { get; set; }
        public int First_Question { get; set; }
        public int Second_Question { get; set; }
        public int Third_Question { get; set; }
        public int Fourth_Question { get; set; }
        public int Fifth_Question { get; set; }
        public int Challenger_Finished_Questions { get; set; }
        public int Defender_Finished_Questions { get; set; }
        public int Answered_Right_Challenger_Questions { get; set; }
        public int Answered_Right_Defender_Questions { get; set; }
        public bool Challenger_Finished { get; set; }
        public bool Defender_Finished { get; set; }
        public bool Defender_Started { get; set; }

        internal void FinishQuestion()
        {
            if(!Challenger_Finished)
            {
                Challenger_Finished_Questions++;
                if (Challenger_Finished_Questions == 5)
                {
                    Challenger_Finished = true;
                }
            } else if (!Defender_Finished)
            {
                Defender_Finished_Questions++;
                if(Defender_Finished_Questions == 5)
                {
                    Defender_Finished = true;
                }
            }
        }

        internal void RightAnswer()
        {
            if (!Challenger_Finished)
            {
                Answered_Right_Challenger_Questions++;
            }
            else if (!Defender_Finished)
            {
                Answered_Right_Defender_Questions++;
            }
        }
    }
}
