using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball
{
    abstract class Screen
    {
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;

        public Screen(GraphicsDeviceManager graphics, ContentManager content)
        {
            this.graphics = graphics;
            this.content = content;
        }

        public abstract bool Update(float dt); // Returns true if screen should be swapped
        public abstract void Draw();
    }
}
