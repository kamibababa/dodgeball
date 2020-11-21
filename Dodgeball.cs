﻿using Dodgeball.Models;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dodgeball
{
    public class Dodgeball : Game
    {
        private GraphicsDeviceManager graphics;
        private World world;
        private Renderer renderer;

        public Dodgeball()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            world = new World();
            renderer = new Renderer(graphics, world);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            renderer.Render();
            base.Draw(gameTime);
        }
    }
}
