using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball
{
    abstract class Screen
    {
        public abstract void Initialize();
        public abstract void Update(float dt);
        public abstract void Render(float dt);
    }
}
