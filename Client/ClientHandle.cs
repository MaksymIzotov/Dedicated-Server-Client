using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ClientHandle
    {
        private static int initialCursorTop;
        private static int initialCursorLeft;

        public static void Welcome(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Console.WriteLine($"Message from server: {_msg}");
            Client.Instance.myId = _myId;

            ClientSend.WelcomeReceived();
        }

        public static void MessageReceived(Packet _packet)
        {
            string _msg = _packet.ReadString();
            string _username = _packet.ReadString();

            AddLine(_msg, _username);
        }

        private static void AddLine(string _msg, string _username)
        {
            initialCursorTop = Console.CursorTop;
            initialCursorLeft = Console.CursorLeft;

            Console.MoveBufferArea(0, initialCursorTop, Console.WindowWidth,
                1, 0, initialCursorTop + 1);
            Console.CursorTop = initialCursorTop;
            Console.CursorLeft = 0;

            Console.WriteLine($"{_username}: {_msg}");

            Console.CursorTop = initialCursorTop + 1;
            Console.CursorLeft = initialCursorLeft;
        }
    }
}
