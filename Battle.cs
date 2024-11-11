using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public class Battle
    {
        public static Card Fight(Card card1, Card card2)
        {

            // Spezialfall: FireElf weicht Drachen aus -> Unentschieden
            if ((card1.Name == "FireElf" && card2.Name == "Dragon") ||
                (card2.Name == "FireElf" && card1.Name == "Dragon"))
            {
                return null;
            }

            // Berechne den Schaden von card1 gegen card2 und umgekehrt
            int damageCard1 = (card1 as IBattleable)?.CalculateDamageAgainst(card2) ?? 0;
            int damageCard2 = (card2 as IBattleable)?.CalculateDamageAgainst(card1) ?? 0;

            // Bestimme den Sieger basierend auf dem Schaden
            if (damageCard1 > damageCard2)
            {
                return card1;  // card1 gewinnt
            }
            else if (damageCard2 > damageCard1)
            {
                return card2;  // card2 gewinnt
            }

            return null;  // Unentschieden
        }
    }

}
