using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Screens
{
    class TitleScreen : Screen
    {
        private const float FlashLength = 0.75f;

        public bool NextLevel;

        private float timer; 
        private Texture2D titleScreenWhite, titleScreenRed;
        private SpriteBatch spriteBatch;

        public TitleScreen(GraphicsDeviceManager graphics, ContentManager content) : base(graphics, content)
        {
            NextLevel = true;
            timer = 0;
            titleScreenWhite = content.Load<Texture2D>("Images/Screens/titleScreenDesktopWhite");
            titleScreenRed = content.Load<Texture2D>("Images/Screens/titleScreenDesktopRed");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override bool Update(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
