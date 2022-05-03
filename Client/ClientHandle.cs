using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ClientHandle
    {
        public static void Welcome(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Console.WriteLine($"Message from server: {_msg}");
            Client.Instance.myId = _myId;

            AddLine();

            ClientSend.WelcomeReceived();
        }

        public static void MessageReceived(Packet _packet)
        {
            string _msg = _packet.ReadString();
            string _username = _packet.ReadString();

            Console.WriteLine($"{_username}: {_msg}");

            AddLine();
        }

        private static void AddLine()
        {
            int initialCursorTop = Console.CursorTop;
            int initialCursorLeft = Console.CursorLeft;

            Console.MoveBufferArea(0, initialCursorTop, Console.WindowWidth,
                1, 0, initialCursorTop + 1);
            Console.CursorTop = initialCursorTop;
            Console.CursorLeft = 0;
            Console.CursorTop = initialCursorTop + 1;
            Console.CursorLeft = initialCursorLeft;
        }
    }
}
