using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Shared.Utilities
{
    public class HelperUtility
    {
        /// <summary>r***@gmail.com  — masks everything between first char and @</summary>
        public string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return string.Empty;

            var atIndex = email.IndexOf('@');
            if (atIndex <= 1) return email;          // too short to mask meaningfully

            // keep first char + "***" + @domain
            return email[0] + "***" + email[atIndex..];
        }

        /// <summary>****3210  — shows last 4 digits only</summary>
        public string MaskMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return string.Empty;

            var digits = mobile.Replace(" ", "").Replace("-", "");
            if (digits.Length <= 4) return digits;   // too short to mask

            return new string('*', digits.Length - 4) + digits[^4..];
        }

        public string GenerateInviteCode()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var random = new Random();

            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // 6-digit OTP
        }
    }
}
