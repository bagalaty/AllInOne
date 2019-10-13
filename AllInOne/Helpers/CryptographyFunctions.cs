using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class CryptographyFunctions
    {
        /// <summary>
        /// 
        /// </summary>
        public void HashFunction()
        {
            //var plainText = "This is a simple documentration of hashing";
            var plainText = "Bogy";

            SHA512 hashSvc = SHA512.Create();

            byte[] hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            Console.WriteLine(hash.ToString());

            var hex = BitConverter.ToString(hash).Replace("-", "");
            Console.WriteLine(hex);

        }

        public void SymmetricFunction() {

            var ChiperText = "";

            Aes chiper = CreateCipher();
            var IV = Convert.ToBase64String(chiper.IV);

            ICryptoTransform cryptTransform = chiper.CreateEncryptor();
            char[] Plaintext = null;
            byte[] plaintext = Encoding.UTF8.GetBytes(Plaintext);
            byte[] cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);

        //    ChiperText = Convert.ToBase64String(chiperText);


        }

        private Aes CreateCipher()
        {
            Aes chiper = Aes.Create();
            chiper.Padding = PaddingMode.ISO10126;

        //    chiper.Key = convertions.HexToByteArray("asdfasdfasdfewrtqwr23412");
            return chiper;
        }
    }
}
