using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2.Button;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game_2.MainMenuNP
{
    class MainMenu : Menu
    {
        #region fields
        
        private List<Button> _buttons;

        private Game1 _game1;

        private WindowManager _windowManager;

        #endregion



        #region methods

        public MainMenu(Game1 pGame1, WindowManager pWindowManager)
        {
            _game1 = pGame1;
            _windowManager = pWindowManager;
        }

        public void createButtons()
        {
            _buttons = new List<Button>();
            _buttons.Add(new Button(_buttonTexture, _font));
            _buttons[0].Text = "Enter Game";
            _buttons[0].Position = new Vector2(550, 300);
            _buttons[0].Click += _enterLobbyButton;
            _buttons.Add(new Button(_buttonTexture, _font));
            _buttons[1].Text = "Exit Game";
            _buttons[1].Position = new Vector2(550, 400);
            _buttons[1].Click += _exit;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach(Button button in _buttons)
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

        private void _enterLobbyButton(object sender, System.EventArgs e)
        {
            _windowManager.CurrentWindow = Windows.LOBBY;
        }

        /// <summary>
        /// Uses the Exit() from Game1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _exit(object sender, System.EventArgs e)
        {
            _game1.ExitProgram();
        }

        #endregion
    }
}
