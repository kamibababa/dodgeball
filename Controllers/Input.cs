using Dodgeball.Models;
using Dodgeball.Screens;
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
        private static int NumPauseOptions = 3;

        public static bool Pause;
        public static bool Throw, Lunge;
        public static bool Restart;
        public static Vector2 ThrowHere, LungeHere;
        public static int PauseSelection; // 1 == restart, 2 == main menu, 3 == back

        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;

        public static void Reset()
        {
            Pause = false;
            Throw = false;
            Restart = false;
            PauseSelection = NumPauseOptions;
        }

        public static void Update(GameScreen.GameState gameState)
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // Look for pause
            if (keyboardState.IsKeyDown(Keys.Escape) && !lastKeyboardState.IsKeyDown(Keys.Escape))
            {
                PauseSelection = NumPauseOptions; // Last option is back
                Pause = !Pause;
            }

            if (gameState == GameScreen.GameState.Playing)
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

            // Pause menu
            else if (gameState == GameScreen.GameState.Paused)
            {
                // Move selection up
                if ((keyboardState.IsKeyDown(Keys.Up) && !lastKeyboardState.IsKeyDown(Keys.Up))
                    || (keyboardState.IsKeyDown(Keys.W) && !lastKeyboardState.IsKeyDown(Keys.W)))
                {
                    if (PauseSelection > 1)
                        PauseSelection--;
                }
                // Move selection down
                if ((keyboardState.IsKeyDown(Keys.Down) && !lastKeyboardState.IsKeyDown(Keys.Down))
                    || (keyboardState.IsKeyDown(Keys.S) && !lastKeyboardState.IsKeyDown(Keys.S)))
                {
                    if (PauseSelection < NumPauseOptions)
                        PauseSelection++;
                }
                // Look for with enter key
                if (keyboardState.IsKeyDown(Keys.Enter) && !lastKeyboardState.IsKeyDown(Keys.Enter))
                {
                    switch (PauseSelection)
                    {
                        case 1: // Restart
                            Restart = true;
                            break;
                        case 2: // Main menu
                            // TODO
                            break;
                        case 3: // Back
                            Pause = !Pause;
                            break;
                    }
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
