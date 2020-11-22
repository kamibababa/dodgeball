using Dodgeball.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private TextureSet textures;

        public Renderer(GraphicsDeviceManager graphics, World world, ContentManager content)
        {
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.world = world;
            renderTarget = new RenderTarget2D(graphicsDevice, world.Width, world.Height);
            textures = new TextureSet(content);

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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp); // Do not linearly interpolate when stretching textures

            // Background
            spriteBatch.Draw(textures.Environments[world.Environment], new Rectangle(0, 0, world.Width, world.Height), Color.White);

            // GameChars
            foreach (GameChar gameChar in world.AllGameChars)
            {
                drawGameChar(gameChar);
            }

            spriteBatch.End();

            // Draw render target to window
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            spriteBatch.End();
        }

        private void drawEntity(Entity entity, Texture2D texture)
        {
            spriteBatch.Draw(texture, entity.Bounds, Color.White);
        }

        private void drawGameChar(GameChar gameChar)
        {
            if (gameChar.Side == GameChar.Team.Left)
            {
                drawEntity(gameChar, textures.GameChars[gameChar.AvatarType]);
            }
            else // Right
            {
                // Flip texture horizontally
                spriteBatch.Draw(textures.GameChars[gameChar.AvatarType],
                    gameChar.Bounds,
                    null,
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    SpriteEffects.FlipHorizontally,
                    0);
            }
        }
    }
}
