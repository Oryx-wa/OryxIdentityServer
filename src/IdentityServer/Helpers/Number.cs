﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Helpers
{
    public static class Number
    {
        public static Decimal Round(Decimal number, int precision = 2)
        {
            return Math.Round(number, precision, MidpointRounding.AwayFromZero);
        }
    }
}
