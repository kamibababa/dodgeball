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
            activeScreen.Update(dt);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            activeScreen.Draw();
            base.Draw(gameTime);
        }
    }
}
