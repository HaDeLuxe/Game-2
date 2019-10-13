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
    class Head : PlayerComponent
    {
        #region fields

        #endregion

        #region properties

        #endregion

        #region methods

        public Head(Texture2D pTexture, Vector2 pPosition, float pRotation) : base(pTexture, pPosition, pRotation)
        {
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, new Vector2(Rectangle.X, Rectangle.Y), null, Color.White, Rotation + (float)Math.PI, new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f), 1f, SpriteEffects.None, 0);
            Console.WriteLine("X: " + CurrentPosition.X + " Y: " + CurrentPosition.Y + " R: " + Rotation);
        }

        //public override void Update(GameTime gameTime)
        //{
        //}
        
        //public override void MoveSnake(object source, ElapsedEventArgs e)
        //{
            
        //    RotateBy(Rotation);
        //    DirectionVector.Normalize();
        //    CurrentPosition -= DirectionVector;
        //}

        //public override bool CheckCollision(Rectangle pRectangle)
        //{
        //    if (Rectangle.Intersects(Rectangle))
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //public override void PreviousPosLogic(object source, ElapsedEventArgs e)
        //{
        //    PreviousPosition = CurrentPosition;
        //    PreviousRotation = Rotation;
        //}




        #endregion
    }
}
