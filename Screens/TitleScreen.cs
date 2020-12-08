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
    class TitleScreen : Screen
    {
        private const int CursorSize = 45;
        private const float FlashLength = 1.5f;

        private float timer; 
        private Texture2D titleScreenWhite, titleScreenRed;
        private SpriteBatch spriteBatch;
        private Texture2D cursor;

        public TitleScreen(GraphicsDeviceManager graphics, ContentManager content) : base(graphics, content)
        {
            timer = 0;
            titleScreenWhite = content.Load<Texture2D>("Images/Screens/titleScreenDesktopWhite");
            titleScreenRed = content.Load<Texture2D>("Images/Screens/titleScreenDesktopRed");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            cursor = content.Load<Texture2D>("Images/cursorRed");
        }

        public override void Draw()
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Texture2D screenTexture;
            if (timer % FlashLength * 2 < FlashLength)
                screenTexture = titleScreenWhite;
            else
                screenTexture = titleScreenRed;
            spriteBatch.Draw(screenTexture, new Rectangle(0, 0, Renderer.WindowWidth, Renderer.WindowHeight), Color.White);

            // Draw cursor
            Rectangle dest = new Rectangle((int)(Mouse.GetState().X - CursorSize / 2),
                (int)(Mouse.GetState().Y - CursorSize / 2),
                CursorSize,
                CursorSize);
            spriteBatch.Draw(cursor, dest, Color.White);

            spriteBatch.End();
        }

        public override bool Update(float dt)
        {
            timer += dt;

            // Move to next screen if keyboard or mouse button is pressed
            if (Keyboard.GetState().GetPressedKeys().Length > 0
                || Mouse.GetState().LeftButton == ButtonState.Pressed
                || Mouse.GetState().MiddleButton == ButtonState.Pressed
                || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }
    }
}
