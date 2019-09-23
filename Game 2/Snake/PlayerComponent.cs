using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Game_2.Snake
{
    abstract class PlayerComponent
    {
        #region fields

        protected Texture2D _texture2D;

        Timer timer;

        #endregion


        #region properties

        public Vector2 CurrentPosition { get; set; }

        public Vector2 NewPosition { get; set; }

        public Vector2 previousPosition { get; set; }


        public Rectangle Rectangle {
            get {
                return new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, _texture2D.Width, _texture2D.Height);
            }
        }

        #endregion

        #region methods

        public PlayerComponent(Texture2D pTexture, Vector2 pPosition)
        {
            _texture2D = pTexture;
            initTimer();
            //CurrentPosition = pPosition;
        }

        public void updatePositions()
        {
            CurrentPosition = NewPosition;
        }

        private void initTimer()
        {
            timer = new Timer(10);
            timer.Elapsed += moveSnake;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public abstract void moveSnake(Object source, ElapsedEventArgs e);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void changeDirection(Enums.directions pDirection);

        public abstract bool CheckFood(Food pFood);
        

        #endregion
    }
}
