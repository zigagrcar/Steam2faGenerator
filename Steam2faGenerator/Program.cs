using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SteamAuth;
using System.Timers;

namespace Steam2faGenerator
{
    class Program
    {
        public static Timer myTimer { get; private set; }

        private static void getCode()
        {
            string sharedSecret = "";

            Console.Clear();

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
        }

        static void Main(string[] args)
        {
            // Create a timer
            myTimer = new System.Timers.Timer();
            // Tell the timer what to do when it elapses
            myTimer.Elapsed += new ElapsedEventHandler(myEvent);
            // Set it to go off every five seconds
            myTimer.Interval = 5000;
            // And start it        
            myTimer.Enabled = true;

            // Get code on start
            getCode();

            Console.ReadKey();
        }

        // Implement a call with the right signature for events going off
        private static void myEvent(object source, ElapsedEventArgs e)
        {
            getCode();
        }
    }
}
