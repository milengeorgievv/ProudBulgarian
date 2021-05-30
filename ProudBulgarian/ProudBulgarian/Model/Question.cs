using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProudBulgarian.Model
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Text { get; set; }
        [MaxLength(50)]
        public string Right_Answer { get; set; }
        [MaxLength(50)]
        public string First_Answer { get; set; }
        [MaxLength(50)]
        public string Second_Answer { get; set; }
        [MaxLength(50)]
        public string Third_Answer { get; set; }
        [MaxLength(50)]
        public string Fourth_Answer { get; set; }
    }
}
