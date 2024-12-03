namespace Swen1.MTCG_Petrovic.Models
{
    public abstract class Card
    {
        public string Id { get; set; }  // Füge diese Eigenschaft hinzu
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Element { get; set; }  // Element type (e.g., "fire", "water", "normal")

        protected Card(string id, string name, int damage, string element)
        {
            Id = id;
            Name = name;
            Damage = damage;
            Element = element;
        }
    }

    public class MonsterCard : Card
    {
        public MonsterCard(string id, string name, int damage, string element)
            : base(id, name, damage, element)
        {
        }
    }

    public class SpellCard : Card
    {
        public SpellCard(string id, string name, int damage, string element)
            : base(id, name, damage, element)
        {
        }
    }
}
