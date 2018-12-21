using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace PASS
{
    //Provides functions for encrypting and decrypting data and storing it in an external file.
    public class DataManager
    {
        public DataManager(string PassPhraseInput, string HashAlgorithmInput)
        {
            passPhrase = PassPhraseInput;
            hashAlgorithm = HashAlgorithmInput;
        }

        public bool Write<T>(T item, string path)
        {
            string output = Encrypt(item);
            try
            {
                File.WriteAllText(path, output);
            }
            catch (Exception) { return false; }
            return true;
        }

        public T Read<T>(string path)
        {
            string data = File.ReadAllText(path);
            return Decrypt<T>(data);
        }

        #region StackOverflow code to encrypt the data being stored
        private readonly string passPhrase = "AnnualShallowBlackWidowSpider";       // can be any string
        private readonly string hashAlgorithm = "SHA1";            // can be "MD5"
        private readonly string saltValue = "sALtValue";       // can be any string
        private readonly int passwordIterations = 7;                  // can be any number
        private readonly string initVector = "~1B2c3D4e5F6g7H8"; // must be 16 bytes
        private readonly int keySize = 256;                // can be 192 or 128

        private string Encrypt<T>(T item)
        {
            string data = ToString(item);
            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[] rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
            RijndaelManaged managed = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform transform = managed.CreateEncryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }

        private T Decrypt<T>(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Convert.FromBase64String(data);
            byte[] rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
            RijndaelManaged managed = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform transform = managed.CreateDecryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            byte[] buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return FromString<T>(Encoding.UTF8.GetString(buffer5, 0, count));
        }
        #endregion

        #region Data Translation Helpers
        public string ToString<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public T FromString<T>(string purexml) { return (T)XmlDeserializeFromString(purexml, typeof(T)); }
        public object XmlDeserializeFromString(string purexml, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(purexml))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
        #endregion
    }
}