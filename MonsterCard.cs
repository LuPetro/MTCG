using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public class MonsterCard : Card
    {
        public MonsterCard(string name, int damage, string element)
            : base(name, damage, element) { }
    }
}
