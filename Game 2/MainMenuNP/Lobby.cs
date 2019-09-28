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

        private bool _connected;

        private Server _server;

        private Client _client;





        #endregion


        #region properties


        #endregion


        #region methods

        public Lobby(WindowManager pWindowManager)
        {
            _windowManager = pWindowManager;
        }

        public void createButtons()
        {
            _buttons = new List<Button>();
            _buttons.Add(new Button(_buttonTexture, _font));
            _buttons[0].Text = "Open Server";
            _buttons[0].Position = new Vector2(1000, 600);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (Button button in _buttons)
            {
                button.draw(gameTime, spriteBatch);
            }
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Button button in _buttons)
            {
                button.update(gameTime);
            }
        }

        #endregion

    }
}
