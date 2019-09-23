using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2
{
    class Food
    {

        #region fields

        private Texture2D _texture2D;

        #endregion

        #region properties

        public Vector2 Position { get; set; }

        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture2D.Width, _texture2D.Height);
            }
        }

        #endregion


        #region methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pTexture"></param>
        public Food(Texture2D pTexture, Vector2 pPosition)
        {
            _texture2D = pTexture;
            Position = pPosition;
        }

        /// <summary>
        /// Responsible for drawing sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, Rectangle, Color.White);
        }
        #endregion
    }
}
