using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Coins { get; set; } = 20;  // Each user starts with 20 coins
        public List<Card> Cards { get; set; } = new List<Card>();  // Collection of all owned cards
        public List<Card> Deck { get; set; } = new List<Card>();   // Active deck for battles

        // Constructor for creating a new user with a specified username and password
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
