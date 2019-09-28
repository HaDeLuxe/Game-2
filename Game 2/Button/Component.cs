using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2.Button
{
    abstract class Component
    {
        public abstract void draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void update(GameTime gameTime);
    }
}
