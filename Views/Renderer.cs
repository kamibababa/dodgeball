using Dodgeball.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Views
{
    class Renderer
    {
        private const int WindowWidth = 1024;
        private const int WindowHeight = 576;

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private World world;

        public Renderer(GraphicsDeviceManager graphics, World world)
        {
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.world = world;

            // Set window size & title
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();
        }

        public void Render()
        {
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.End();
        }
    }
}
