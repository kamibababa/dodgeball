using Dodgeball.Controllers;
using Dodgeball.Models;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Screens
{
    class GameScreen : Screen
    {
        public enum GameState
        {
            Ready, Playing, Paused, LevelOver
        }

        private const float ReadyLength = 3.0f;

        private GameState gameState;
        private float timer, levelOverTimer;
        private World world;
        private ControllerSet controllers;
        private Renderer renderer;

        public GameScreen(GraphicsDeviceManager graphics, ContentManager content, World.Day day) : base(graphics, content)
        {
            timer = -ReadyLength; // Game starts at 0
            levelOverTimer = 0;
            world = new World(day);
            controllers = new ControllerSet(world);
            renderer = new Renderer(graphics, world, content);
        }

        public override void Draw()
        {
            renderer.Render(gameState, levelOverTimer);
        }

        public override void Update(float dt)
        {
            setGameState(dt);
            Input.Update(gameState);
            if (gameState == GameState.Playing)
                controllers.Update(dt);
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

    }
}
