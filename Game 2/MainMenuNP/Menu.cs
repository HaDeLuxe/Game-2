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

        private Texture2D _buttonTexture;

        private SpriteFont _font;
        
        #endregion



        #region properties

        #endregion



        #region methods

        public Menu()
        {
        }

        public void LoadSprites(ContentManager content)
        {
            _background = content.Load<Texture2D>("background");
            _buttonTexture = content.Load<Texture2D>("button");
            _font = content.Load<SpriteFont>("Bebas_Regular");
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        }

        public virtual void Update()
        {

        }

        #endregion
    }
}
