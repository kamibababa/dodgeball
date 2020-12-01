using Dodgeball.Controllers;
using Dodgeball.Models;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Dodgeball
{
    public class Dodgeball : Game
    {
        public enum GameState
        {
            Ready, Playing, Paused, LevelOver
        }

        private const float ReadyLength = 3.0f;

        private GameState gameState;
        private float timer, levelOverTimer;
        private GraphicsDeviceManager graphics;
        private World world;
        private ControllerSet controllers;
        private Renderer renderer;

        public Dodgeball()
        {
            gameState = GameState.Ready;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            timer = -ReadyLength; // Game starts at 0
            levelOverTimer = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            world = new World(World.Day.Mon);
            controllers = new ControllerSet(world);
            renderer = new Renderer(graphics, world, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            setGameState(dt);
            Input.Update(gameState);
            if (gameState == GameState.Playing)
                controllers.Update(dt);
            base.Update(gameTime);
        }

        private void setGameState(float dt)
        {
            // Update timers
            if (gameState == GameState.Ready || gameState == GameState.Playing)
            {
                timer += dt;
            }
            else if (gameState == GameState.LevelOver)
            {
                levelOverTimer += dt;
            }

            // Ready to Playing
            if (gameState == GameState.Ready && timer >= 0)
            {
                gameState = GameState.Playing;
            }

            // Ready || Playing to Paused
            if (gameState == GameState.Ready || gameState == GameState.Playing)
            {
                if (Input.Pause)
                {
                    gameState = GameState.Paused;
                }
            }

            // Paused to Ready || Playing
            if (gameState == GameState.Paused)
            {
                if (!Input.Pause)
                {
                    if (timer < 0)
                        gameState = GameState.Ready;
                    else
                        gameState = GameState.Playing;
                }
            }

            // Level over
            if (world.Enemies.Count == 0 || world.Player.Health == 0)
            {
                gameState = GameState.LevelOver;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            renderer.Render(gameState, levelOverTimer);
            base.Draw(gameTime);
        }
    }
}
