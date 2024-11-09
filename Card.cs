using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public abstract class Card
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Element { get; set; }  // Element type (e.g., "fire", "water", "normal")

        public Card(string name, int damage, string element)
        {
            Name = name;
            Damage = damage;
            Element = element;
        }
    }
}
