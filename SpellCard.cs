using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public class SpellCard : Card
    {
        public SpellCard(string name, int damage, string element)
            : base(name, damage, element) { }
    }
}
