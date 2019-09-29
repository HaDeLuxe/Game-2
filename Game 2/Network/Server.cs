using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Game_2.Network
{

    enum sendMessageType
    {
        GET_NUMBER_PLAYER_IN_GAME,
        DISCOVERY
    }


    class Server
    {
        #region fields

        private NetServer _server;

        private NetworkGame netGame1;

        #endregion

        #region methods

        public void StartServer()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Game 2")
            { Port = 12345 };

            _server = new NetServer(config);
            _server.Start();
            checkForMessages();

            netGame1 = new NetworkGame();
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
        }
        
        public void closingServer()
        {
            _server.Shutdown("bye");
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
                        string data = msg.ReadString();
                        switch (data)
                        {
                            case "Connect To Game":
                                joinGame(msg.SenderConnection.RemoteUniqueIdentifier);
                                break;
                            case "Get number of players in Game":
                                sendMsg(sendMessageType.GET_NUMBER_PLAYER_IN_GAME, msg.SenderConnection);
                                break;
                        }
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
                    case NetIncomingMessageType.DiscoveryRequest:
                        //Create a response
                        NetOutgoingMessage response = _server.CreateMessage();
                        response.Write("Server is here");
                        _server.SendDiscoveryResponse(response, msg.SenderEndPoint);
                        break;
                    default:
                        Console.WriteLine("unhandled message with type: " + msg.MessageType);
                        break;
                }
            }
        }

        public void sendMsg(sendMessageType pSendMsgType, NetConnection pReceiver)
        {
            var msg = _server.CreateMessage();
            switch (pSendMsgType)
            {
                case sendMessageType.GET_NUMBER_PLAYER_IN_GAME:
                    msg.Write("Numbers of players in Game: " + netGame1.numberOfPlayer.ToString());
                    _server.SendMessage(msg, pReceiver, NetDeliveryMethod.ReliableOrdered);

                    break;
            }
        }


        public void joinGame(long pIdentifier)
        {
            if(netGame1.Player1 == 0)
            {
                netGame1.Player1 = pIdentifier;
            }
            else if(netGame1.Player2 == 0)
            {
                netGame1.Player2 = pIdentifier;
            }
        }

        #endregion

    }
}
