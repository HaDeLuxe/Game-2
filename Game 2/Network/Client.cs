using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2.Network
{
    class Client
    {
        #region fields

        private NetClient _client;

        #endregion


        #region methods

        public void startClient()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Game 2");
            _client = new NetClient(config);
            _client.Start();
        }

        public void connectToServer()
        {
            _client.Connect(host: "127.0.0.1", port: 12345);
        }

        public bool checkForMessages()
        {
            NetIncomingMessage msg;
            while ((msg = _client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        //handle custom messages
                        return false;
                    case NetIncomingMessageType.StatusChanged:
                        //handle connection status mesasages
                        switch (msg.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                return true;
                            default:
                                return false;
                        }
                    case NetIncomingMessageType.DebugMessage:
                        //handle Debug messages
                        //only received when compiled in DEBUG mode
                        Console.WriteLine(msg.ReadString());
                        return false;

                    default:
                        Console.WriteLine("unhandled message with type: " + msg.MessageType);
                        return false;
                }
            }
            return false;
        }


        #endregion

    }
}
