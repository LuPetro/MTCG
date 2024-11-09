using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public class Package
    {
        public List<Card> Cards { get; set; } = new List<Card>();  // Collection of 5 cards
        public int Price { get; } = 5;  // Cost in coins

        public Package(List<Card> cards)
        {
            if (cards.Count == 5)
                Cards = cards;
            else
                throw new ArgumentException("A package must contain exactly 5 cards.");
        }
    }
}
