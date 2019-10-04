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
        public enum MsgType
        {
            SERVER_ONLINE,
            CLIENT_CONNECTED,
            PLAYER_COUNT_1,
            PLAYER_COUNT_2,
            PLAYER_COUNT_0,
            JOINED_GAME_SUCCESS,
            JOINED_GAME_FAILURE,
            NOT_SPECIFIC
        }

        public enum SendMsgType
        {
            CONNECT_TO_GAME,
            ENTER_GAME,
            GET_NUMBER_PLAYER_IN_GAME,
            SEND_PLAYER_POS,
            SEND_BODY_POS,

        }


        #region fields

        private NetClient _client;


        #endregion


        #region properties
        

        #endregion

        #region methods

        public void StartClient()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Game 2");
            config.EnableUPnP = true;
            _client = new NetClient(config);
            _client.Start();
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            // attempt to forward port 14242
            _client.UPnP.ForwardPort(12345, "Text detail here");

        }

        public void ConnectToServer()
        {
            _client.Connect(host: "127.0.0.1", port: 12345);
        }
        

        public MsgType CheckForMessages()
        {
            NetIncomingMessage msg;
            while ((msg = _client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                            return MsgType.SERVER_ONLINE;
                    case NetIncomingMessageType.Data:
                        string data = msg.ReadString();
                        switch (data)
                        {
                            case "Numbers of players in Game: 2":
                                return MsgType.PLAYER_COUNT_2;
                            case "Numbers of players in Game: 1":
                                return MsgType.PLAYER_COUNT_1;
                            case "Numbers of players in Game: 0":
                                return MsgType.PLAYER_COUNT_0;
                            case "JOINED_GAME_SUCCESS":
                                return MsgType.JOINED_GAME_SUCCESS;
                            case "JOINED_GAME_FAILURE":
                                return MsgType.JOINED_GAME_FAILURE;

                        }
                            //handle custom messages
                            return MsgType.NOT_SPECIFIC;
                    case NetIncomingMessageType.StatusChanged:
                        //handle connection status mesasages
                        switch (msg.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                return MsgType.CLIENT_CONNECTED;
                            default:
                                return MsgType.NOT_SPECIFIC;
                        }
                    case NetIncomingMessageType.DebugMessage:
                        //handle Debug messages
                        //only received when compiled in DEBUG mode
                        Console.WriteLine(msg.ReadString());
                        return MsgType.NOT_SPECIFIC;
                    
                    default:
                        Console.WriteLine("unhandled message with type: " + msg.MessageType);
                        return MsgType.NOT_SPECIFIC;
                }
                
            }
            return MsgType.NOT_SPECIFIC;
        }

        

        public void CheckForServer()
        {
            _client.DiscoverKnownPeer("127.0.0.1", 12345);
        }

        public void SendMsg(SendMsgType pSendMsgType)
        {
            var message = _client.CreateMessage();
            switch (pSendMsgType)
            {
                case SendMsgType.CONNECT_TO_GAME:
                    message.Write("Connect To Game");
                    break;
                case SendMsgType.GET_NUMBER_PLAYER_IN_GAME:
                    message.Write("Get number of players in Game");
                    break;
            }
            _client.SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }


        #endregion

    }
}
