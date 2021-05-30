using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProudBulgarian.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public int Duels_Won { get; set; }
        public int Duels_Played { get; set; }
        public int Single_Player_Progress { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
        //public List<int> Friends { get; set; }
        //public List<int> Duels_Received { get; set; }
        //public List<int> Duels_Requested { get; set; }
    }
}
