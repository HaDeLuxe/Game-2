using Game_2.MainMenuNP;
using Game_2.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        

        private MainMenu _mainMenu;

        private GameManager _gameManager;

        private Lobby _lobby;




        #endregion



        #region properties

        public windows CurrentWindow { get; set; }

        #endregion



        #region methods

        public WindowManager(Game1 game1, Server pServer, Client pClient)
        {
            CurrentWindow = windows.PLAYFIELD;

            _mainMenu = new MainMenu(game1, this);

            _lobby = new Lobby(this, pServer, pClient, game1);

            _gameManager = new GameManager();
        }

        public void LoadContent(ContentManager Content)
        {
            _mainMenu.LoadSprites(Content);
            _lobby.LoadSprites(Content);
            _gameManager.LoadContent(Content);
            _mainMenu.createButtons();
            _lobby.createButtons();
            

        }

        public void Update(GameTime gameTime)
        {
            switch (CurrentWindow)
            {
                case windows.MAINMENU:
                    _mainMenu.Update(gameTime);
                    break;
                case windows.LOBBY:
                    _lobby.Update(gameTime);
                    break;
                case windows.PLAYFIELD:
                    _gameManager.Update(gameTime);
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (CurrentWindow)
            {
                case windows.MAINMENU:
                    _mainMenu.Draw(gameTime, spriteBatch);
                    break;
                case windows.LOBBY:
                    _lobby.Draw(gameTime, spriteBatch);
                    break;
                case windows.PLAYFIELD:
                    _gameManager.Draw(gameTime, spriteBatch);
                    break;
            }
        }

        #endregion
    }
}
