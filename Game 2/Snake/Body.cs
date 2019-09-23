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

        public override bool CheckFood()
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, Rectangle, Color.White);


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
