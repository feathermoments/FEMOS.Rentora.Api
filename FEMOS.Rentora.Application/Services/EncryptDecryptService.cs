using FEMOS.IdentifyManagement;
using FEMOS.Rentora.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class EncryptDecryptService : IEncryptDecryptService
    {
        #region AES-256 Encryption / Decryption
        private readonly EncryptDecrypt objEncryptDecrypt = new EncryptDecrypt();
        public string Encrypt(string plainText, string base64Key = null)
        {
            return objEncryptDecrypt.EncryptAES256(plainText);
        }

        public string Decrypt(string cipherText, string base64Key = null)
        {
            return objEncryptDecrypt.DecryptAES256(cipherText);
        }
        #endregion

        #region HMACSHA256 Hash
        public string ComputeHash(string input, string key = null)
        {
            return objEncryptDecrypt.ComputeHMACSHA256(input, key);
        }
        #endregion
    }
}
