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
    class Menu
    {
        #region fields

        private Texture2D _background;

        protected Texture2D _buttonTexture;

        protected SpriteFont _font;
        
        #endregion



        #region properties

        #endregion



        #region methods

        public Menu()
        {
        }

        public virtual void LoadSprites(ContentManager content)
        {
            _background = content.Load<Texture2D>("Background");
            _buttonTexture = content.Load<Texture2D>("button");
            _font = content.Load<SpriteFont>("Arial");
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
