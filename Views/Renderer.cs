﻿using Dodgeball.Controllers;
using Dodgeball.Models;
using Dodgeball.Screens;
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
        public const int WindowWidth = 1280;
        public const int WindowHeight = 720;

        private const int Scale = 4; // Ratio of virtual size to native texture size
        private const float VeryLowHealth = .2f;
        private const float LowHealth = .4f;
        private const float MediumHealth = .6f;
        private const int CursorSize = 36;
        private const float PopupFlashLength = 0.75f;

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
            renderTarget = new RenderTarget2D(graphicsDevice, World.Width, World.Height);
            textures = new TextureSet(content);

            whiteRect = new Texture2D(graphicsDevice, 1, 1);
            whiteRect.SetData(new[] { Color.White });
        }

        public void Render(GameScreen.GameState gameState, float levelOverTimer)
        {
            graphicsDevice.Clear(Color.Black);

            // Draw to render target
            graphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp); // Do not linearly interpolate when stretching textures

            // Background
            spriteBatch.Draw(textures.Environments[world.Environment], new Rectangle(0, 0, World.Width, World.Height), Color.White);

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
                drawInventory(gameChar);
                drawLungeBar(gameChar);
            }

            drawPopUps(gameState, levelOverTimer);
            drawCursor();

            spriteBatch.End();

            // Draw render target to window
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            spriteBatch.End();
        }

        private void drawPopUps(GameScreen.GameState gameState, float levelOverTimer)
        {
            // Ready
            if (gameState == GameScreen.GameState.Ready)
            {
                drawTextureAtCenter(textures.ReadyText);
            }

            // Paused
            else if (gameState == GameScreen.GameState.Paused)
            {
                drawTextureAtCenter(textures.PauseMenu[Input.PauseSelection]);
            }

            // Level over
            else if (gameState == GameScreen.GameState.LevelOver)
            {
                // Winner
                if (world.Enemies.Count == 0)
                {
                    if (levelOverTimer % (PopupFlashLength * 2) < PopupFlashLength)
                        drawTextureAtCenter(textures.WinnerTextWhite);
                    else
                        drawTextureAtCenter(textures.WinnerTextRed);
                }

                // Loser
                else if (world.Player.Health <= 0)
                {
                    if (levelOverTimer % (PopupFlashLength * 2) < PopupFlashLength)
                        drawTextureAtCenter(textures.LoserTextWhite);
                    else
                        drawTextureAtCenter(textures.LoserTextRed);
                }
            }
        }

        // Draw a texture at the center of the screen
        private void drawTextureAtCenter(Texture2D texture)
        {
            Rectangle dest = new Rectangle(
                World.Width / 2 - texture.Width * Scale / 2,
                World.Height / 2 - texture.Height * Scale / 2,
                texture.Width * Scale,
                texture.Height * Scale);
            spriteBatch.Draw(texture, dest, Color.White);
        }

        // Render mouse cursor
        private void drawCursor()
        {
            Texture2D cursor = textures.CursorGray;
            foreach (GameChar enemy in world.Enemies)
            {
                if (enemy.Bounds.Contains(Input.MouseVirtualPos))
                {
                    cursor = textures.CursorRed;
                    break;
                }
            }
            Rectangle dest = new Rectangle((int)(Input.MouseVirtualPos.X - CursorSize / 2),
                (int)(Input.MouseVirtualPos.Y - CursorSize / 2),
                CursorSize,
                CursorSize);
            spriteBatch.Draw(cursor, dest, Color.White);
        }

        private void drawLungeBar(GameChar gameChar)
        {
            Color color;
            if (gameChar.LungeCooldown > 0)
                color = Color.Gray;
            else
                color = Color.White;

            float lungePercentage = (GameChar.LungeCooldownLength - gameChar.LungeCooldown) / GameChar.LungeCooldownLength;
            Rectangle lungeBar = gameChar.Side == GameChar.Team.Left ? UI.LeftLungeBar : UI.RightLungeBar;
            Rectangle rect = new Rectangle(
                lungeBar.X + gameChar.Bounds.X,
                lungeBar.Y + gameChar.Bounds.Y + (int)(lungeBar.Height * (1.0 - lungePercentage)),
                lungeBar.Width,
                (int) (lungeBar.Height * lungePercentage));
            drawRect(rect, color);
        }

        private void drawInventory(GameChar gameChar)
        {
            for (int i = 0; i < gameChar.MaxBallsHeld; i++)
            {
                Color color;
                if (gameChar.BallsHeld <= i)
                    color = Color.Gray;
                else
                    color = Color.Red;

                Rectangle inventory = gameChar.Side == GameChar.Team.Left ? UI.LeftInventory : UI.RightInventory;

                Rectangle rect = new Rectangle(
                    inventory.X + gameChar.Bounds.X,
                    inventory.Y + gameChar.Bounds.Y + UI.InventorySpacing * i,
                    inventory.Width,
                    inventory.Height);
                drawRect(rect, color);
            }
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
