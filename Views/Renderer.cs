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
        private const int WindowWidth = 1280;
        private const int WindowHeight = 720;
        private const float VeryLowHealth = .2f;
        private const float LowHealth = .4f;
        private const float MediumHealth = .6f;

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private World world;
        private RenderTarget2D renderTarget;
        private TextureSet textures;
        private Texture2D whiteRect; // Used for rendering rectangles

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

            whiteRect = new Texture2D(graphicsDevice, 1, 1);
            whiteRect.SetData(new[] { Color.White });
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

            // Balls
            foreach (Ball ball in world.Balls)
            {
                drawBall(ball);
            }

            // UI
            foreach (GameChar gameChar in world.AllGameChars)
            {
                drawHealthBar(gameChar);
            }

            spriteBatch.End();

            // Draw render target to window
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            spriteBatch.End();
        }

        private void drawHealthBar(GameChar gameChar)
        {
            // Set color
            Color color;
            float healthPercentage = (float) gameChar.Health / gameChar.MaxHealth;
            if (healthPercentage <= VeryLowHealth)
                color = Color.Red;
            else if (healthPercentage <= LowHealth)
                color = Color.Orange;
            else if (healthPercentage <= MediumHealth)
                color = Color.Yellow;
            else
                color = Color.Green;

            Rectangle rect = new Rectangle(
                UI.HealthBar.X + gameChar.Bounds.X,
                UI.HealthBar.Y + gameChar.Bounds.Y,
                (int)(UI.HealthBar.Width * healthPercentage),
                UI.HealthBar.Height);
            drawRect(rect, color);
        }

        private void drawRect(Rectangle rect, Color color)
        {
            spriteBatch.Draw(whiteRect, rect, color);
        }

        private void drawBall(Ball ball)
        {
            Texture2D texture;
            if (ball.IsAlive)
                texture = textures.BallAlive;
            else
                texture = textures.BallDead;
            drawEntity(ball, texture);
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
