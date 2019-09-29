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

        private Timer _timer;
        private Timer _timer2;


        #endregion

        #region properties

        public Vector2 CurrentPosition { get; set; }

        public Vector2 NewPosition { get; set; }

        public Vector2 PreviousPosition { get; set; }

        public float Rotation { get; set; }

        public float PreviousRotation { get; set; }

        public Vector2 DirectionVector { get; set; }



        public Rectangle Rectangle {
            get {
                return new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, _texture2D.Width, _texture2D.Height);
            }
        }

        #endregion

        #region methods

        public PlayerComponent(Texture2D pTexture, Vector2 pPosition, float pRotation)
        {
            _texture2D = pTexture;
            _initTimer();
            _initTimer2();
            CurrentPosition = pPosition;
            PreviousPosition = pPosition;
            Rotation = pRotation;
            PreviousRotation = pRotation;
        }

        public void RotateBy(float a)
        {
            DirectionVector = new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
        }
       
        private void _initTimer()
        {
            _timer = new Timer(10);
            _timer.Elapsed += MoveSnake;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void _initTimer2()
        {
            _timer2 = new Timer(1000);
            _timer2.Elapsed += PreviousPosLogic;
            _timer2.AutoReset = true;
            _timer2.Enabled = true;
        }

        public abstract void PreviousPosLogic(Object source, ElapsedEventArgs e);
       
        public abstract void MoveSnake(Object source, ElapsedEventArgs e);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public virtual bool CheckCollision(Rectangle pRectangle)
        {
            return false;
        }
        
        

        #endregion
    }
}
