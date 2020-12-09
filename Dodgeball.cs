using Dodgeball.Controllers;
using Dodgeball.Models;
using Dodgeball.Screens;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Dodgeball
{
    public class Dodgeball : Game
    {
        private GraphicsDeviceManager graphics;
        private Screen activeScreen;

        public Dodgeball()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // Set window size & title
            graphics.PreferredBackBufferWidth = Renderer.WindowWidth;
            graphics.PreferredBackBufferHeight = Renderer.WindowHeight;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            activeScreen = new TitleScreen(graphics, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            if (activeScreen.Update(dt))
                swapScreen();
        }

        private void swapScreen()
        {
            // TitleScreen
            if (activeScreen is TitleScreen)
            {
                activeScreen = new LevelSelectScreen(graphics, Content);
            }

            // LevelSelectScreen
            else if (activeScreen is LevelSelectScreen)
            {
                activeScreen = new GameScreen(graphics, Content, ((LevelSelectScreen)activeScreen).SelectedDay.Value);
            }
            
            // GameScreen
            else if (activeScreen is GameScreen)
            {
                if (((GameScreen)activeScreen).RestartLevel)
                    activeScreen = new GameScreen(graphics, Content, ((GameScreen)activeScreen).DayOfWeek);
                else if (((GameScreen)activeScreen).NextLevel)
                {
                    // Final level
                    if (((GameScreen)activeScreen).DayOfWeek == World.Day.Fri)
                        activeScreen = new TitleScreen(graphics, Content);
                    else
                        activeScreen = new GameScreen(graphics, Content, ((GameScreen)activeScreen).DayOfWeek + 1);
                }
                else if (((GameScreen)activeScreen).GoToLevelSelect)
                    activeScreen = new LevelSelectScreen(graphics, Content);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            activeScreen.Draw();
            base.Draw(gameTime);
        }
    }
}
