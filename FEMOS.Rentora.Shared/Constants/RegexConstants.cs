using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Shared.Constants
{
    public static class RegexConstants
    {
        public const string Mobile =
            @"^[6-9]\d{9}$";

        public const string Email =
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    }
}
