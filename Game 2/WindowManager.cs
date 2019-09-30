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

    public enum Windows {
        MAINMENU,
        LOBBY,
        PLAYFIELD
    }


    class WindowManager
    {
        #region fields
        

        private readonly MainMenu _mainMenu;

        private readonly GameManager _gameManager;

        private readonly Lobby _lobby;




        #endregion



        #region properties

        public Windows CurrentWindow { get; set; }

        #endregion



        #region methods

        public WindowManager(Game1 game1, Client pClient)
        {
            CurrentWindow = Windows.MAINMENU;

            _mainMenu = new MainMenu(game1, this);

            _lobby = new Lobby(this, pClient, game1);

            _gameManager = new GameManager();
        }

        public void LoadContent(ContentManager Content)
        {
            _mainMenu.LoadSprites(Content);
            _lobby.LoadSprites(Content);
            _gameManager.LoadContent(Content);
            _mainMenu.createButtons();
            _lobby.CreateButtons();
            

        }

        public void Update(GameTime gameTime)
        {
            switch (CurrentWindow)
            {
                case Windows.MAINMENU:
                    _mainMenu.Update(gameTime);
                    break;
                case Windows.LOBBY:
                    _lobby.Update(gameTime);
                    break;
                case Windows.PLAYFIELD:
                    _gameManager.Update(gameTime);
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (CurrentWindow)
            {
                case Windows.MAINMENU:
                    _mainMenu.Draw(gameTime, spriteBatch);
                    break;
                case Windows.LOBBY:
                    _lobby.Draw(gameTime, spriteBatch);
                    break;
                case Windows.PLAYFIELD:
                    _gameManager.Draw(gameTime, spriteBatch);
                    break;
            }
        }

        #endregion
    }
}
