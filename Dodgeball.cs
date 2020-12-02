using Dodgeball.Controllers;
using Dodgeball.Models;
using Dodgeball.Screens;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Dodgeball
{
    public class Dodgeball : Game
    {
        private GraphicsDeviceManager graphics;
        private Screen activeScreen;

        public Dodgeball()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            activeScreen = new GameScreen(graphics, Content, World.Day.Mon);
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            if (activeScreen.Update(dt))
                swapScreen();
        }

        private void swapScreen()
        {
            // GameScreen
            if (activeScreen is GameScreen)
            {
                if (((GameScreen)activeScreen).RestartLevel)
                    activeScreen = new GameScreen(graphics, Content, ((GameScreen)activeScreen).DayOfWeek);
                else if (((GameScreen)activeScreen).NextLevel)
                    activeScreen = new GameScreen(graphics, Content, ((GameScreen)activeScreen).DayOfWeek + 1);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            activeScreen.Draw();
            base.Draw(gameTime);
        }
    }
}
