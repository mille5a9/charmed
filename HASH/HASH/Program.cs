using System;
using System.Text;
using System.Security.Cryptography;

namespace HASH
{
    public class HashDetector
    {
        public static string Get(string input, string output)
        {
            input = input.Replace("\0", string.Empty);

            //SHA-1 check
            using (SHA1 mySHA1 = SHA1.Create())
            {
                byte[] bytes = mySHA1.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                if (builder.ToString() == output) return "SHA-1";
            }
            //SHA-256 check
            using (SHA256 mysha256 = SHA256.Create())
            {
                byte[] bytes = mysha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                if (builder.ToString() == output) return "SHA-256";
            }
            //SHA-384 check
            using (SHA384 mySHA384 = SHA384.Create())
            {
                byte[] bytes = mySHA384.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                if (builder.ToString() == output) return "SHA-384";
            }
            //SHA-512 check
            using (SHA512 mySHA512 = SHA512.Create())
            {
                byte[] bytes = mySHA512.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                if (builder.ToString() == output) return "SHA-512";
            }

            using (MD5 myMD5 = MD5.Create())
            {
                byte[] bytes = myMD5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                if (builder.ToString() == output) return "MD5";
            }
            return "Err: Unknown Algorithm";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Hash Function Detector!\n");
            while (true)
            {
                Console.WriteLine("Please input some data:");
                string data = Console.ReadLine();
                Console.WriteLine("Please input the result of hashing the data:");
                string result = Console.ReadLine();
                result = HashDetector.Get(data, result);
                Console.WriteLine("The data was hashed with: " + result);
                Console.ReadLine();
            }
        }
    }
}
