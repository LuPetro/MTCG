﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic
{
    public interface IBattleable
    {
        int CalculateDamageAgainst(Card opponent);
    }
}