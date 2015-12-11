using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SteamAuth;

namespace Steam2faGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string sharedSecret = "";

            Console.Title = "Steam2faGenerator";
            Console.WriteLine("Generating 2Fa codes");

            try
            {
                if (!File.Exists("shared_secret.txt"))
                {
                    throw new FileNotFoundException();
                }
                sharedSecret = File.ReadAllText(@"shared_secret.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                if (sharedSecret.Length == 0)
                {
                    throw new FileLoadException();
                }
                SteamGuardAccount account = new SteamGuardAccount();
                account.SharedSecret = sharedSecret;
                string code = account.GenerateSteamGuardCode();
                Console.WriteLine("Code: " + code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
