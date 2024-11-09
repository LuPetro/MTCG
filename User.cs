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

        // Methode, um eine Karte zum Deck hinzuzufügen
        public bool AddCardToDeck(Card card)
        {
            if (Deck.Count >= 4)
            {
                Console.WriteLine("Das Deck ist voll. Du kannst maximal 4 Karten im Deck haben.");
                return false;
            }

            if (!Cards.Contains(card))
            {
                Console.WriteLine("Die Karte befindet sich nicht in deinem Besitz.");
                return false;
            }

            Deck.Add(card);
            return true;
        }

        // Methode, um eine Karte aus dem Deck zu entfernen
        public bool RemoveCardFromDeck(Card card)
        {
            if (Deck.Contains(card))
            {
                Deck.Remove(card);
                return true;
            }

            Console.WriteLine("Die Karte befindet sich nicht im Deck.");
            return false;
        }

        public bool PurchasePackage(Package package)
        {
            if (Coins < package.Price)
            {
                Console.WriteLine("Nicht genügend Coins, um das Paket zu kaufen.");
                return false;
            }

            Coins -= package.Price;
            Cards.AddRange(package.Cards);  // Fügt die Karten aus dem Paket zum Besitz des Benutzers hinzu
            return true;
        }

    }
}
