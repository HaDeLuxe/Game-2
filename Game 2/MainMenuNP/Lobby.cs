using Game_2.Network;
using Game2.Button;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2.MainMenuNP
{
    struct NetworkGame
    {
        public bool active;
        public short numberOfPlayerJoined;
    }

    class Lobby : Menu
    {

        #region fields

        private readonly WindowManager _windowManager;

        private bool _connectedToServer;

        private bool _serverAvailable;

        private bool _enteredGame;
        
        
        private readonly Client _client;

        private readonly Game1 _game1;
        

        #region buttons
        
        private Button _joinServerButton;
        private Button _connectNetworkButton;
        private Button _enterGameButton;

        #endregion


        #endregion

        #region properties

        public NetworkGame netWorkGame1;

        #endregion
        
        #region methods

        public Lobby(WindowManager pWindowManager, Client pClient, Game1 pGame1)
        {
            _windowManager = pWindowManager;
            _client = pClient;
            _game1 = pGame1;
            _connectedToServer = false;
            netWorkGame1 = new NetworkGame();
        }

        public void CreateButtons()
        {

            _joinServerButton = new Button(_buttonTexture, _font)
            {
                Text = "Join Server",
                Position = new Vector2(900, 500)
            };
            _joinServerButton.Click += _joinServer;

            _connectNetworkButton = new Button(_buttonTexture, _font)
            {
                Text = "Join Game",
                Position = new Vector2(700, 195)
            };
            _connectNetworkButton.Click += _connectNetworkGame1;

            _enterGameButton = new Button(_buttonTexture, _font)
            {
                Text = "Enter Game",
                Position = new Vector2(700, 195)
            };
            _enterGameButton.Click += _enterGame;
        }
        
        private void _joinServer(object sender, System.EventArgs e)
        {
            _client.ConnectToServer();
        }

        private void _connectNetworkGame1(object sender, System.EventArgs e)
        {
            _client.SendMsg(Client.SendMessageType.CONNECT_TO_GAME);
        }

        private void _enterGame(object sender, System.EventArgs e)
        {
            _client.SendMsg(Client.SendMessageType.ENTER_GAME);
            _windowManager.CurrentWindow = Windows.PLAYFIELD;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (!_enteredGame)
                _connectNetworkButton.draw(gameTime, spriteBatch);
            else
                _enterGameButton.draw(gameTime, spriteBatch);
            if(!_connectedToServer)
                _joinServerButton.draw(gameTime, spriteBatch);

            spriteBatch.DrawString(_font, "Client Connected: " + _connectedToServer.ToString(), new Vector2(200, 200), Color.Black);
            spriteBatch.DrawString(_font, "Server Status: " + _serverAvailable.ToString(), new Vector2(200, 250), Color.Black);

            spriteBatch.DrawString(_font, "Game 1: " + netWorkGame1.numberOfPlayerJoined.ToString() + " players in Game", new Vector2(500, 200), Color.Black);
            spriteBatch.DrawString(_font, "Game 1 Running: " + netWorkGame1.active + " ", new Vector2(500, 220), Color.Black);


        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_serverAvailable)
                _client.CheckForServer();

            if (_connectedToServer)
            {
                _client.SendMsg(Client.SendMessageType.GET_NUMBER_PLAYER_IN_GAME);
            }

            Client.MsgType var = _client.CheckForMessagesLobby();
            switch (var)
            {
                case Client.MsgType.CLIENT_CONNECTED:
                    _connectedToServer = true;
                    break;
                case Client.MsgType.SERVER_ONLINE:
                    _serverAvailable = true;
                    break;
                case Client.MsgType.PLAYER_COUNT_0:
                    netWorkGame1.numberOfPlayerJoined = 0;
                    break;
                case Client.MsgType.PLAYER_COUNT_1:
                    netWorkGame1.numberOfPlayerJoined = 1;
                    break;
                case Client.MsgType.PLAYER_COUNT_2:
                    netWorkGame1.numberOfPlayerJoined = 2;
                    break;
                case Client.MsgType.JOINED_GAME_SUCCESS:
                    _enteredGame = true;
                    break;
                case Client.MsgType.JOINED_GAME_FAILURE:
                    break;


            }

            
            if(!_connectedToServer)
                _joinServerButton.update(gameTime);
            if (!_enteredGame)
                _connectNetworkButton.update(gameTime);
            else
                _enterGameButton.update(gameTime);
            if (!_serverAvailable)
                _client.CheckForServer();
            
        }

        #endregion

    }
}
