using Dodgeball.Models;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Screens
{
    class LevelSelectScreen : Screen
    {
        private Rectangle[] TileLocations = new Rectangle[]
        {
            new Rectangle(65, 186, 350, 210),
            new Rectangle(465, 186, 350, 210),
            new Rectangle(865, 186, 350, 210),
            new Rectangle(265, 436, 350, 210),
            new Rectangle(665, 436, 350, 210)
        };
        private Rectangle RestartLocation = new Rectangle(1048, 440, 200, 200);

        public World.Day? SelectedDay; // Nullable enum

        private SpriteBatch spriteBatch;
        private Texture2D levelSelectScreen;
        private Dictionary<World.Day, Texture2D> whiteTiles, redTiles;
        private KeyboardState keyboardState, lastKeyboardState;
        private MouseState mouseState, lastMouseState;
        private bool isFirstFrame;

        public LevelSelectScreen(GraphicsDeviceManager graphics, ContentManager content) : base(graphics, content)
        {
            SelectedDay = World.Day.Mon;
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            isFirstFrame = true;

            // Load textures
            levelSelectScreen = content.Load<Texture2D>("Images/Screens/levelSelectScreen");
            whiteTiles = new Dictionary<World.Day, Texture2D>();
            whiteTiles.Add(World.Day.Mon, content.Load<Texture2D>("Images/Screens/mondayTileWhite"));
            whiteTiles.Add(World.Day.Tue, content.Load<Texture2D>("Images/Screens/tuesdayTileWhite"));
            whiteTiles.Add(World.Day.Wed, content.Load<Texture2D>("Images/Screens/wednesdayTileWhite"));
            whiteTiles.Add(World.Day.Thu, content.Load<Texture2D>("Images/Screens/thursdayTileWhite"));
            whiteTiles.Add(World.Day.Fri, content.Load<Texture2D>("Images/Screens/fridayTileWhite"));
            redTiles = new Dictionary<World.Day, Texture2D>();
            redTiles.Add(World.Day.Mon, content.Load<Texture2D>("Images/Screens/mondayTileRed"));
            redTiles.Add(World.Day.Tue, content.Load<Texture2D>("Images/Screens/tuesdayTileRed"));
            redTiles.Add(World.Day.Wed, content.Load<Texture2D>("Images/Screens/wednesdayTileRed"));
            redTiles.Add(World.Day.Thu, content.Load<Texture2D>("Images/Screens/thursdayTileRed"));
            redTiles.Add(World.Day.Fri, content.Load<Texture2D>("Images/Screens/fridayTileRed"));
        }

        public override void Draw()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(levelSelectScreen, new Rectangle(0, 0, Renderer.WindowWidth, Renderer.WindowHeight), Color.White);

            // Render day tiles
            foreach (World.Day day in Enum.GetValues(typeof(World.Day)))
            {
                Texture2D tile;
                if (SelectedDay == day)
                    tile = redTiles[day];
                else
                    tile = whiteTiles[day];
                spriteBatch.Draw(tile, TileLocations[(int)day], Color.White);
            }

            spriteBatch.End();
        }

        public override bool Update(float dt)
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (!isFirstFrame) // Prevents click-thru from TitleScreen
            {
                // Mouse click
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed)
                {
                    Vector2 mousePos = new Vector2(mouseState.X, mouseState.Y);
                    World.Day? newSelection = null;
                    for (int i = 0; i < World.NumDays; i++)
                    {
                        if (TileLocations[i].Contains(mousePos))
                        {
                            newSelection = (World.Day)i;
                        }
                    }

                    if (newSelection != null)
                    {
                        if (newSelection == SelectedDay) // Selection confirmed
                            return true;
                        else // Change selection
                            SelectedDay = newSelection.Value;
                    }
                }

                // TODO Keyboard
            }

            isFirstFrame = false;

            return false;
        }
    }
}
