using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public class MonsterCard : Card, IBattleable
    {
        public MonsterCard(string name, int damage, string element)
            : base(name, damage, element) { }

        public int CalculateDamageAgainst(Card opponent)
        {
            // Spezialregel: Goblins greifen keine Drachen an
            if (this.Name == "Goblin" && opponent.Name == "Dragon")
            {
                return 0;  // Goblin macht keinen Schaden gegen Drachen
            }

            // Spezialregel: Wizzards kontrollieren Orks, kein Schaden vom Ork
            if (this.Name == "Wizzard" && opponent.Name == "Ork")
            {
                return int.MaxValue;  // Maximaler Schaden, um den Ork sofort zu besiegen
            }

            // Elementareffektivität wird nur bei SpellCards berücksichtigt
            return this.Damage;
        }
    }

}
