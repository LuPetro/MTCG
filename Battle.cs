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
