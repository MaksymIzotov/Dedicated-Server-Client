using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        private static bool isRunning = false;

        private static bool isAuth = false;

        static void Main(string[] args)
        {
            Console.Title = "Client";
            isRunning = true;
            isAuth = false;

            Console.WriteLine("Username: ");
            Client.Instance.username = Console.ReadLine();
            Console.WriteLine("Email: ");
            Client.Instance.email = Console.ReadLine();

            Client.Instance.ConnectToServer();

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
        }

        public static void StartInputThread()
        {
            Thread inputThread = new Thread(new ThreadStart(InputThread));
            inputThread.Start();
        }

        public static void OTPCheck()
        {
            Console.WriteLine("One-Time-Password: ");
            string otp = Console.ReadLine();

            try
            {
                ClientSend.SendOTP(Convert.ToInt32(otp));
            }
            catch
            {
                Console.WriteLine("Wrong Input");
                OTPCheck();
            }
        }

        private static void InputThread()
        {
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                        string message = Console.ReadLine();

                        // Delete input text
                        int linesOfInput = 1 + (message.Length / Console.BufferWidth);
                        //Move cursor to just before the input just entered
                        Console.CursorTop -= linesOfInput;
                        Console.CursorLeft = 0;
                        //blank out the content that was just entered
                        Console.WriteLine(new string(' ', message.Length));
                        //move the cursor to just before the input was just entered
                        Console.CursorTop -= linesOfInput;
                        Console.CursorLeft = 0;

                        //Send message
                        ClientSend.SendMessage(message, Client.Instance.username);

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    ThreadManager.UpdateMain();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }       
    }
}
