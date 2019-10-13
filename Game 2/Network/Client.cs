using Game_2.Snake;
using Lidgren.Network;
using Microsoft.Xna.Framework;
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

        public enum SendMessageType
        {
            CONNECT_TO_GAME,
            ENTER_GAME,
            GET_NUMBER_PLAYER_IN_GAME,
            SEND_PLAYER_POS,
            SEND_BODY_POS,
            INPUT_LEFT,
            INPUT_RIGHT,
            JOIN_SERVER

        }


        #region fields

        private NetClient _client;

        private WindowManager _windowManager;

        #endregion


        #region properties
        
        public short PlayerNumber { get; set; }

        #endregion

        #region methods

        public void InitWindowManager(WindowManager pWindowManager)
        {
            _windowManager = pWindowManager;
        }

        public void StartClient()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Game 2");
            config.EnableUPnP = true;
            _client = new NetClient(config);
            _client.Start();
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            PlayerNumber = 0;

        }

       
        public void ConnectToServer()
        {
            
            _client.Connect(host: "127.0.0.1", port: 8080);
            SendMsg(SendMessageType.JOIN_SERVER);
        }
        

        public MsgType CheckForMessagesLobby()
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
                            case "JOINED_GAME_SUCCESS_PLAYER_1":
                                PlayerNumber = 1;
                                return MsgType.JOINED_GAME_SUCCESS;
                            case "JOINED_GAME_SUCCESS_PLAYER_2":
                                PlayerNumber = 2;
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

        public MsgType CheckForMessagesGameManager(List<PlayerComponent> pPlayerList, List<PlayerComponent> pEnemyList)
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
                            case "MOVE":
                                int x = msg.ReadVariableInt32();
                                int y = msg.ReadVariableInt32();
                                pPlayerList[0].CurrentPosition = new Vector2(x, y);
                                pPlayerList[0].Rotation = msg.ReadFloat();
                                int eX = msg.ReadVariableInt32();
                                int eY = msg.ReadVariableInt32();
                                pEnemyList[0].CurrentPosition = new Vector2(eX, eY);
                                pEnemyList[0].Rotation = msg.ReadFloat();


                                return MsgType.NOT_SPECIFIC;
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
            _client.DiscoverKnownPeer("127.0.0.1", 8080);
        }

        public void SendMsg(SendMessageType pSendMsgType)
        {
            var message = _client.CreateMessage();
            switch (pSendMsgType)
            {
                case SendMessageType.CONNECT_TO_GAME:
                    message.Write("Connect To Game");
                    break;
                case SendMessageType.GET_NUMBER_PLAYER_IN_GAME:
                    message.Write("Get number of players in Game");
                    break;
                case SendMessageType.JOIN_SERVER:
                    break;
                case SendMessageType.ENTER_GAME:
                    message.Write("ENTER_GAME");
                    break;
            }
            _client.SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Server Sided Network Messages while Main Game is running
        /// </summary>
        /// <param name="pSendMsgType"></param>
        /// <param name="pReceiver"></param>
        public void SendMainGameMsg(SendMessageType pSendMsgType)
        {
            var msg = _client.CreateMessage();
            {
                switch (pSendMsgType)
                {
                    case SendMessageType.INPUT_LEFT:
                        msg.Write("LEFT");
                        break;
                    case SendMessageType.INPUT_RIGHT:
                        msg.Write("RIGHT");
                        break;
                }
                _client.SendMessage(msg, NetDeliveryMethod.Unreliable);
            }
        }


        #endregion

    }
}
