using Game_2.MainMenuNP;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2
{

    public enum windows {
        MAINMENU,
        LOBBY,
        PLAYFIELD
    }


    class WindowManager
    {
        #region fields

        private windows _currentWindow;

        private MainMenu _mainMenu;

        private GameManager _gameManager;

        private Lobby _lobby;


        #endregion



        #region properties

        #endregion



        #region methods

        public WindowManager()
        {
            _currentWindow = windows.MAINMENU;

            _mainMenu = new MainMenu();

            _lobby = new Lobby();

        }

        public void Update()
        {
            switch (_currentWindow)
            {
                case windows.MAINMENU:

                    break;
                case windows.LOBBY:

                    break;
                case windows.PLAYFIELD:

                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (_currentWindow)
            {
                case windows.MAINMENU:
                    _mainMenu.Draw(gameTime, spriteBatch);
                    break;
                case windows.LOBBY:
                    _lobby.Draw(gameTime, spriteBatch);
                    break;
                case windows.PLAYFIELD:

                    break;
            }
        }

        #endregion
    }
}
