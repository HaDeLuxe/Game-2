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

        private Vector2 _moveDir;

        private float _currentRot;

        #endregion

        #region properties


        #endregion


        #region methods

        public Body(Texture2D pTexture, Vector2 pPosition, float pRotation) : base(pTexture, pPosition, pRotation)
        {
        }

        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture2D, new Vector2(Rectangle.X, Rectangle.Y), null, Color.White, _currentRot, new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f), 1f, SpriteEffects.None, 0);
        }

        public override void MoveSnake(object source, ElapsedEventArgs e)
        {
                CurrentPosition -= _moveDir;

            _currentRot = Lerp(PreviousRotation, Rotation, 100000);
        }

        public override void PreviousPosLogic(object source, ElapsedEventArgs e)
        {
            PreviousPosition = CurrentPosition;
            PreviousRotation = Rotation;
            _moveDir = PreviousPosition - NewPosition;
            _moveDir.Normalize();



        }

        float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public override void Update(GameTime gameTime)
        {
        }

        #endregion
    }
}
