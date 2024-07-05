
using System.Text;
using System.Security.Cryptography;


namespace PizzaCase
{
    internal static class Encryption
    {
        // based on the example by microsoft with some minor changes for ease of use
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0

        private static readonly int iterations = 1000;
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("placeholder");

        /// <summary>
        /// takes an encrypted string, key and an initialization vector and decrypts the string, returning the plain text
        /// </summary>
        /// <param name="encryptedString"> the text that will be decrypted</param>
        /// <param name="key">the encryption key</param>
        /// <param name="InitalizationVector">sets the initialization vector. This number is needed by the encryption algorithm to correctly encrypt the data.</param>
        /// <returns>encrypted data</returns>
        /// <exception cref="ArgumentNullException"> throws on null imput</exception>
        public static string Decrypt(byte[] encryptedString, string key, string InitalizationVector) {

            Console.WriteLine(encryptedString);

            // check arguments
            {
                if (encryptedString == null || encryptedString.Length <= 0)
                    throw new ArgumentNullException(nameof(encryptedString));

                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException(nameof(key));

                if (InitalizationVector == null || InitalizationVector.Length <= 0)
                    throw new ArgumentNullException(nameof(InitalizationVector));
            }


            // create a new instance of the default C# Advanced Encryption Standard
            Aes aes = Aes.Create();

            // convert arguments to byte arrays
            byte[] encrypted_bytes = encryptedString;
            byte[] key_bytes = Encoding.Unicode.GetBytes(key);
            byte[] iv_bytes = Encoding.Unicode.GetBytes(InitalizationVector);

            // aes only accepts keys that are of a certain byte size.
            // for easier use we will derive a key of the correct size using Rfc2898DeriveBytes
            // this generates a hash of our key that has the correct size
            key_bytes = Rfc2898DeriveBytes.Pbkdf2(key_bytes,
                salt, // we can optionally salt this hash,
                      // but since it's only for internal use I don't see the point.
                iterations,
                HashAlgorithmName.SHA256,
                32); //amount of derived bytes we want

            //same thing for the initalization vector
            iv_bytes = Rfc2898DeriveBytes.Pbkdf2(iv_bytes, salt, iterations, HashAlgorithmName.SHA256, 16);

            aes.Key = key_bytes;
            // setting the initialization vector
            // https://en.wikipedia.org/wiki/Initialization_vector
            aes.IV = iv_bytes;

            //create decryption stream
            string plainText;
            using (MemoryStream stream = new MemoryStream(encrypted_bytes))
            {
                using (CryptoStream decrypt = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(decrypt))
                    {
                        plainText = streamReader.ReadToEnd();
                    }
                }
            }
            
            return plainText;
        }
    }
}
