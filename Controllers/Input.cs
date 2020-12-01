using Dodgeball.Models;
using Dodgeball.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    static class Input
    {
        public static bool Throw = false, Lunge = false;
        public static Vector2 ThrowHere, LungeHere;

        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;

        public static void Update(Dodgeball.GameState gameState)
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (gameState == Dodgeball.GameState.Playing)
            {
                // Throw
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed)
                {
                    Throw = true;
                    // Scale window coordinates to world coordinates
                    ThrowHere = new Vector2(
                        (float)mouseState.X / Renderer.WindowWidth * World.Width,
                        (float)mouseState.Y / Renderer.WindowHeight * World.Height);
                }
                // Lunge
                else if (mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton != ButtonState.Pressed)
                {
                    Lunge = true;
                    // Scale window coordinates to world coordinates
                    LungeHere = new Vector2(
                        (float)mouseState.X / Renderer.WindowWidth * World.Width,
                        (float)mouseState.Y / Renderer.WindowHeight * World.Height);
                }
            }
        }

        public static bool MoveLeft
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left);
            }
        }

        public static bool MoveRight
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right);
            }
        }

        public static bool MoveUp
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up);
            }
        }

        public static bool MoveDown
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down);
            }
        }

        public static Vector2 MouseVirtualPos
        {
            get
            {
                return new Vector2(
                    (float)mouseState.X / Renderer.WindowWidth * World.Width,
                    (float)mouseState.Y / Renderer.WindowHeight * World.Height);
            }
        }
    }
}
