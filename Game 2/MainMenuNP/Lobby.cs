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
    class Lobby : Menu
    {

        #region fields
        private List<Button> _buttons;

        private WindowManager _windowManager;

        private bool _connectedToServer;
        

        private Server _server;

        private Client _client;
        

        #endregion


        #region properties


        #endregion


        #region methods

        public Lobby(WindowManager pWindowManager, Server pServer, Client pClient)
        {
            _windowManager = pWindowManager;
            _server = pServer;
            _client = pClient;

            _connectedToServer = false;
        }

        public void createButtons()
        {
            _buttons = new List<Button>();
            _buttons.Add(new Button(_buttonTexture, _font));
            _buttons[0].Text = "Open Server";
            _buttons[0].Position = new Vector2(1000, 600);
            _buttons[0].Click += _startServer;

            _buttons.Add(new Button(_buttonTexture, _font));
            _buttons[1].Text = "Join Server";
            _buttons[1].Position = new Vector2(1000, 500);
            _buttons[1].Click += _joinServer;

        }

        private void _startServer(object sender, System.EventArgs e)
        {
            _server.StartServer();
        }

        private void _joinServer(object sender, System.EventArgs e)
        {
            _client.connectToServer();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (Button button in _buttons)
            {
                button.draw(gameTime, spriteBatch);
            }


            spriteBatch.DrawString(_font, "Server Status: " + _connectedToServer.ToString(), new Vector2(200, 200), Color.Black);
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_client.checkForMessages())
                _connectedToServer = true;

            foreach (Button button in _buttons)
            {
                button.update(gameTime);
            }
        }

        #endregion

    }
}
