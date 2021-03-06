using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ClientSend
    {
        private static void SendTCPData(Packet _packet)
        {
            _packet.WriteLength();
            Client.Instance.tcp.SendData(_packet);
        }

        #region Packets
        public static void WelcomeReceived()
        {
            using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(Client.Instance.myId);
                _packet.Write(Client.Instance.username);

                SendTCPData(_packet);
            }
        }

        public static void SendOTP(int otp)
        {
            using (Packet _packet = new Packet((int)ServerPackets.otpReceived))
            {
                _packet.Write(otp);
                _packet.Write(Client.Instance.username);

                SendTCPData(_packet);
            }
        }

        public static void SendEmail()
        {
            using (Packet _packet = new Packet((int)ServerPackets.emailReceived))
            {
                _packet.Write(Client.Instance.email);
                _packet.Write(Client.Instance.username);

                SendTCPData(_packet);
            }
        }

        public static void SendMessage(string _msg, string _username)
        {
            using (Packet _packet = new Packet((int)ClientPackets.playerMessage))
            {
                _packet.Write(_msg);
                _packet.Write(_username);

                SendTCPData(_packet);
            }
        }
        #endregion
    }
}
