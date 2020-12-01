using Dodgeball.Controllers;
using Dodgeball.Models;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dodgeball
{
    public class Dodgeball : Game
    {
        public enum GameState
        {
            Ready, Playing, Paused, LevelOver
        }

        private GameState gameState;
        private GraphicsDeviceManager graphics;
        private World world;
        private ControllerSet controllers;
        private Renderer renderer;

        public Dodgeball()
        {
            gameState = GameState.Playing;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            Input.Update(gameState);
            if (gameState == GameState.Playing)
                controllers.Update(dt);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            renderer.Render();
            base.Draw(gameTime);
        }
    }
}
