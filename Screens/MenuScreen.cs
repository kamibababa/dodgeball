using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Screens
{
    abstract class MenuScreen : Screen
    {
        protected const int CursorSize = 45;

        protected SpriteBatch spriteBatch;
        protected KeyboardState keyboardState, lastKeyboardState;
        protected MouseState mouseState, lastMouseState;

        public MenuScreen(GraphicsDeviceManager graphics, ContentManager content) : base(graphics, content)
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
        }

        // Update input states
        protected void getInput()
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        // Render given cursor texture to screen
        protected void drawCursor(Texture2D cursor)
        {
            Rectangle dest = new Rectangle((int)(Mouse.GetState().X - CursorSize / 2),
                (int)(Mouse.GetState().Y - CursorSize / 2),
                CursorSize,
                CursorSize);
            spriteBatch.Draw(cursor, dest, Color.White);
        }
    }
}
