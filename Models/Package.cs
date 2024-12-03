using System.Collections.Generic;

namespace Swen1.MTCG_Petrovic.Models
{
    public class Package
    {
        private static List<Package> _packages = new();

        public string Id { get; private set; }
        public List<Card> Cards { get; private set; } = new();

        private Package(string id, List<Card> cards)
        {
            Id = id;
            Cards = cards;
        }

        public static void Create(string id, List<Card> cards)
        {
            if (cards.Count != 5)
            {
                throw new Exception("Ein Paket muss genau 5 Karten enthalten.");
            }

            Package newPackage = new(id, cards);
            _packages.Add(newPackage);
        }

        public static List<Package> GetAll()
        {
            return _packages;
        }
    }
}
