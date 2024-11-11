using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public class SpellCard : Card, IBattleable
    {
        public SpellCard(string name, int damage, string element)
            : base(name, damage, element) { }

        public int CalculateDamageAgainst(Card opponent)
        {
            // Spezialregel: Kraken sind immun gegen Zauber
            if (opponent.Name == "Kraken")
            {
                return 0;  // Kraken nimmt keinen Schaden von Zaubern
            }

            // Spezialregel: Ritter ertrinken bei Wassersprüchen
            if (this.Element == "water" && opponent.Name == "Knight")
            {
                return int.MaxValue;  // Maximaler Schaden, um den Ritter sofort zu besiegen
            }

            // Spezialregel: FireElves weichen Drachen aus
            if (opponent.Name == "Dragon" && this.Name == "FireElf")
            {
                return 0;  // FireElf nimmt keinen Schaden von Drachen
            }

            // Berechnung der Elementareffektivität
            int effectiveDamage = this.Damage;
            if ((this.Element == "water" && opponent.Element == "fire") ||
                (this.Element == "fire" && opponent.Element == "normal") ||
                (this.Element == "normal" && opponent.Element == "water"))
            {
                effectiveDamage *= 2;  // Verdoppelter Schaden bei effektiven Angriffen
            }
            else if ((this.Element == "fire" && opponent.Element == "water") ||
                     (this.Element == "normal" && opponent.Element == "fire") ||
                     (this.Element == "water" && opponent.Element == "normal"))
            {
                effectiveDamage /= 2;  // Halbierter Schaden bei ineffektiven Angriffen
            }

            return effectiveDamage;
        }
    }

}
