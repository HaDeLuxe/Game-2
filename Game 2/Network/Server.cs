using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2.Network
{
    class Server
    {
        #region fields

        private NetServer _server;

        #endregion

        #region methods

        public void StartServer()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Game 2")
            { Port = 12345 };

            _server = new NetServer(config);
            _server.Start();
        }
        
        public void checkForMessages()
        {
            NetIncomingMessage msg;
            while((msg = _server.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        //handle custom messages
                       
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        //handle connection status mesasages
                        switch (msg.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                break;
                        }
                        break;
                    case NetIncomingMessageType.DebugMessage:
                        //handle Debug messages
                        //only received when compiled in DEBUG mode
                        Console.WriteLine(msg.ReadString());
                        break;

                    default:
                        Console.WriteLine("unhandled message with type: " + msg.MessageType);
                        break;
                }
            }
        }

        #endregion

    }
}
