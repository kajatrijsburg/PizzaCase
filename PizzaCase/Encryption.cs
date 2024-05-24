
using System.Text;
using System.Security.Cryptography;


namespace PizzaCase
{
    internal static class Encryption
    {
        // based on the example by microsoft with some minor changes for ease of use
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0

        private static readonly int iterations = 1000;
        private static readonly byte[] salt = Encoding.UTF8.GetBytes("placeholder");

        /// <summary>
        /// Takes plain text, a key and an initialization vector and encrypts it, returning the result as a string.
        /// </summary>
        /// <param name="plainText"> the text that will be encrypted</param>
        /// <param name="key">the encryption key</param>
        /// <param name="InitalizationVector">sets the initialization vector. This number is needed by the encryption algorithm to correctly encrypt the data.</param>
        /// <returns>encrypted data</returns>
        /// <exception cref="ArgumentNullException"> throws on null imput</exception>
        public static string encrypt(string plainText, string key, string InitalizationVector)
        {
            // check arguments
            {
                if (plainText == null || plainText.Length <= 0)
                    throw new ArgumentNullException(nameof(plainText));

                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException(nameof(key));

                if (InitalizationVector == null || InitalizationVector.Length <= 0)
                    throw new ArgumentNullException(nameof(InitalizationVector));
            }

            // create a new instance of the default C# Advanced Encryption Standard
            Aes aes = Aes.Create();

            // convert arguments to byte arrays
            byte[] key_bytes = Convert.FromBase64String(key);
            byte[] iv_bytes = Convert.FromBase64String(InitalizationVector);

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

            // create encryption stream
            byte[] encrypted_bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream encrypt = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(encrypt))
                    {
                        streamWriter.Write(plainText);
                    }
                   
                    encrypted_bytes = stream.ToArray();
                }
                
            }

            return Convert.ToBase64String(encrypted_bytes);
        }

        /// <summary>
        /// takes an encrypted string, key and an initialization vector and decrypts the string, returning the plain text
        /// </summary>
        /// <param name="plainText"> the text that will be encrypted</param>
        /// <param name="key">the encryption key</param>
        /// <param name="InitalizationVector">sets the initialization vector. This number is needed by the encryption algorithm to correctly encrypt the data.</param>
        /// <returns>encrypted data</returns>
        /// <exception cref="ArgumentNullException"> throws on null imput</exception>
        public static string decrypt(string encryptedString, string key, string InitalizationVector) {
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
            byte[] encrypted_bytes = Convert.FromBase64String(encryptedString);
            byte[] key_bytes = Convert.FromBase64String(key);
            byte[] iv_bytes = Convert.FromBase64String(InitalizationVector);

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
