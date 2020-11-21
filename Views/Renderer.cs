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
        private RenderTarget2D renderTarget;

        public Renderer(GraphicsDeviceManager graphics, World world)
        {
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.world = world;
            renderTarget = new RenderTarget2D(graphicsDevice, world.Width, world.Height);

            // Set window size & title
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();
        }

        public void Render()
        {
            graphicsDevice.Clear(Color.Black);

            // Draw to render target
            graphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin();

            spriteBatch.End();

            // Draw render target to window
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            spriteBatch.End();
        }
    }
}
