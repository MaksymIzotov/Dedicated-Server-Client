using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        private static bool isRunning = false;

        static void Main(string[] args)
        {
            Console.Title = "Client";
            isRunning = true;

            Console.WriteLine("Username: ");
            Client.Instance.username = Console.ReadLine();

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Thread inputThread = new Thread(new ThreadStart(InputThread));
            inputThread.Start();

            Client.Instance.ConnectToServer();
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
    }
}
