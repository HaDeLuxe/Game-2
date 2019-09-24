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

        public Enums.directions Direction { get; set; }

        #endregion

        #region methods

        public Head(Texture2D pTexture, Vector2 pPosition, Enums.directions pDirection) : base(pTexture, pPosition)
        {
            Direction = pDirection;
        }


        float rotation = 0;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (Direction)
            {
                case Enums.directions.Left:
                    rotation = (float) -Math.PI/2;
                    break;
                case Enums.directions.Up:
                    rotation = 0;
                    break;
                case Enums.directions.Down:
                    rotation = (float)Math.PI;
                    break;
                case Enums.directions.Right:
                    rotation = (float)Math.PI/2;
                    break;
            }
            spriteBatch.Draw(_texture2D, new Vector2(Rectangle.X, Rectangle.Y), null, Color.White, rotation, new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f), 1f, SpriteEffects.None, 0);

        }

        public override void Update(GameTime gameTime)
        {
        }

        
        public override void changeDirection(Enums.directions pDirection)
        {
            if((pDirection == Enums.directions.Right && Direction != Enums.directions.Left) ||
                (pDirection == Enums.directions.Left && Direction != Enums.directions.Right) ||
                (pDirection == Enums.directions.Up && Direction != Enums.directions.Down) ||
                (pDirection == Enums.directions.Down && Direction != Enums.directions.Up))  
                    Direction = pDirection;
        }

        public override void moveSnake(object source, ElapsedEventArgs e)
        {
            PreviousPosition = CurrentPosition;
            switch (Direction)
            {
                case Enums.directions.Left:
                    CurrentPosition = new Vector2(CurrentPosition.X - 2, CurrentPosition.Y);
                    break;
                case Enums.directions.Right:
                    CurrentPosition = new Vector2(CurrentPosition.X + 2, CurrentPosition.Y);
                    break;
                case Enums.directions.Up:
                    CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y - 2);
                    break;
                case Enums.directions.Down:
                    CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + 2);
                    break;
            }
        }

        public override bool CheckFood(Food pFood)
        {
            if (Rectangle.Intersects(pFood.Rectangle))
            {
                return true;
            }
            else
                return false;
        }




        #endregion
    }
}
