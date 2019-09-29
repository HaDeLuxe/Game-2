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

        private WindowManager _windowManager;

        private bool _connectedToServer;

        private bool _serverAvailable;
        

        private Server _server;

        private Client _client;

        private Game1 _game1;

        #region buttons

        private Button _openServerButton;
        private Button _joinServerButton;
        private Button _joinGameButton;

        #endregion


        #endregion



        #region properties

        public NetworkGame netWorkGame1;

        #endregion


        #region methods

        public Lobby(WindowManager pWindowManager, Server pServer, Client pClient, Game1 pGame1)
        {
            _windowManager = pWindowManager;
            _server = pServer;
            _client = pClient;
            _game1 = pGame1;
            _connectedToServer = false;
            netWorkGame1 = new NetworkGame();

           

        }

        public void createButtons()
        {
            _openServerButton = new Button(_buttonTexture, _font);
            _openServerButton.Text = "Open Server";
            _openServerButton.Position = new Vector2(900, 600);
            _openServerButton.Click += _startServer;

            _joinServerButton = new Button(_buttonTexture, _font);
            _joinServerButton.Text = "Join Server";
            _joinServerButton.Position = new Vector2(900, 500);
            _joinServerButton.Click += _joinServer;

            _joinGameButton = new Button(_buttonTexture, _font);
            _joinGameButton.Text = "Join Game";
            _joinGameButton.Position = new Vector2(700, 195);
            _joinGameButton.Click += _joinNetworkGame1;
        }

        private void _startServer(object sender, System.EventArgs e)
        {
            _server.StartServer();
            _game1.ServerRunning = true;
        }

        private void _joinServer(object sender, System.EventArgs e)
        {
            _client.connectToServer();
        }

        private void _joinNetworkGame1(object sender, System.EventArgs e)
        {
            _client.sendMsg(Client.sendMsgType.CONNECT_TO_GAME);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            _joinGameButton.draw(gameTime, spriteBatch);
            _joinServerButton.draw(gameTime, spriteBatch);
            _openServerButton.draw(gameTime, spriteBatch);

            spriteBatch.DrawString(_font, "Client Connected: " + _connectedToServer.ToString(), new Vector2(200, 200), Color.Black);
            spriteBatch.DrawString(_font, "Server Status: " + _serverAvailable.ToString(), new Vector2(200, 250), Color.Black);

            spriteBatch.DrawString(_font, "Game 1: " + netWorkGame1.numberOfPlayerJoined.ToString() + " players in Game", new Vector2(500, 200), Color.Black);
            spriteBatch.DrawString(_font, "Game 1 Running: " + netWorkGame1.active + " ", new Vector2(500, 220), Color.Black);


        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_serverAvailable)
                _client.checkForServer();

            if (_connectedToServer)
            {
                _client.sendMsg(Client.sendMsgType.GET_NUMBER_PLAYER_IN_GAME);
            }

            Client.MsgType var = _client.checkForMessages();
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


            }

            
            _joinServerButton.update(gameTime);
            _joinGameButton.update(gameTime);
            if (!_serverAvailable)
                _openServerButton.update(gameTime);
            
            if (!_serverAvailable)
                _client.checkForServer();
        }

        #endregion

    }
}
