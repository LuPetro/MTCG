using System;
using System.Collections.Generic;

namespace Swen1.MTCG_Petrovic.Models
{
    public class User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Card> Cards { get; private set; } = new();
        public int Coins { get; set; } = 20;
        public List<Card> Deck { get; private set; } = new();

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        private static Dictionary<string, User> _users = new();

        public static void Register(string username, string password)
        {
            if (_users.ContainsKey(username))
            {
                throw new Exception("Benutzername existiert bereits.");
            }

            User newUser = new(username, password);
            _users[username] = newUser;
        }

        public static (bool Success, User? User) Authenticate(string username, string password)
        {
            if (_users.TryGetValue(username, out User? user) && user.Password == password)
            {
                return (true, user);
            }
            return (false, null);
        }

        public static User? Get(string username)
        {
            _users.TryGetValue(username, out User? user);
            return user;
        }

    }
}
