using Dodgeball.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Screens
{
    class LevelSelectScreen : Screen
    {
        public World.Day? SelectedDay; // Nullable enum

        private SpriteBatch spriteBatch;
        private Texture2D levelSelectScreen;
        private Texture2D[] whiteTiles, redTiles;
           
        public LevelSelectScreen(GraphicsDeviceManager graphics, ContentManager content) : base(graphics, content)
        {
            SelectedDay = null;
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            // Load textures
            levelSelectScreen = content.Load<Texture2D>("Images/Screens/levelSelectScreen");
            whiteTiles = new Texture2D[Enum.GetValues(typeof(World.Day)).Length];
            whiteTiles[0] = content.Load<Texture2D>("Images/Screens/mondayTileWhite");
            whiteTiles[1] = content.Load<Texture2D>("Images/Screens/tuesdayTileWhite");
            whiteTiles[2] = content.Load<Texture2D>("Images/Screens/wednesdayTileWhite");
            whiteTiles[3] = content.Load<Texture2D>("Images/Screens/thursdayTileWhite");
            whiteTiles[4] = content.Load<Texture2D>("Images/Screens/fridayTileWhite");
            redTiles = new Texture2D[Enum.GetValues(typeof(World.Day)).Length];
            redTiles[0] = content.Load<Texture2D>("Images/Screens/mondayTileRed");
            redTiles[1] = content.Load<Texture2D>("Images/Screens/tuesdayTileRed");
            redTiles[2] = content.Load<Texture2D>("Images/Screens/wednesdayTileRed");
            redTiles[3] = content.Load<Texture2D>("Images/Screens/thursdayTileRed");
            redTiles[4] = content.Load<Texture2D>("Images/Screens/fridayTileRed");
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override bool Update(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
