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
            world = new World(World.Day.Mon);
            renderer = new Renderer(graphics, world, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            renderer.Render();
            base.Draw(gameTime);
        }
    }
}
