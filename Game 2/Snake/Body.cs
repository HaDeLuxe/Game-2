using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_2.Snake
{
    class Body : PlayerComponent
    {
        #region fields


        #endregion

        #region properties


        #endregion


        #region methods

        public Body(Texture2D pTexture, Vector2 pPosition) : base(pTexture, pPosition)
        {
        }

        public override void changeDirection(Enums.directions pDirection)
        {
            throw new NotImplementedException();
        }
        

        public override bool CheckFood(Food pFood)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture2D, new Vector2(Rectangle.X, Rectangle.Y), null, Color.White, 0, new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f), 1f, SpriteEffects.None, 0);
        }

        public override void moveSnake(object source, ElapsedEventArgs e)
        {
            previousPosition = CurrentPosition;
            CurrentPosition = NewPosition;
        }

        public override void Update(GameTime gameTime)
        {
            CurrentPosition = NewPosition;
        }

        #endregion
    }
}
