using System;
using System.Security.Cryptography;
using System.Text;

namespace whostpos.Core.Helpers
{
    public class MiraculousMethods
    {
        public static string Sha256Encrypt(string phrase)
        {
            var encoder = new UTF8Encoding();
            var sha256Hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256Hasher.ComputeHash(encoder.GetBytes(phrase));
            return ByteArrayToString(hashedDataBytes);
        }

        public static string ToBarcodeEN78(string productCode)
        {
            Int64 code = 0;
            Int64.TryParse(productCode, out code);
            return code.ToString("0000000");
        }

        #region Private Methods
        private static string ByteArrayToString(byte[] inputArray)
        {
            var output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }
        #endregion
    }
}
