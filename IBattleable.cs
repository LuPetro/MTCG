using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swen1.MTCG_Petrovic.Models;

namespace Swen1.MTCG_Petrovic
{
    public interface IBattleable
    {
        int CalculateDamageAgainst(Card opponent);
    }
}
