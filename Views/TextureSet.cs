using Dodgeball.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Views
{
    class TextureSet
    {
        public Dictionary<World.Enviro, Texture2D> Environments;
        public Dictionary<GameChar.Avatar, Texture2D> GameChars;
        public Texture2D BallAlive, BallDead;
        public Texture2D CursorRed, CursorGray;
        public Texture2D ReadyText;
        public Texture2D WinnerTextRed, WinnerTextWhite;
        public Texture2D LoserTextRed, LoserTextWhite;
        public Texture2D GameOverTextRed, GameOverTextWhite;
        public Texture2D[] PauseMenu;

        public TextureSet(ContentManager content)
        {
            // Load environments
            Environments = new Dictionary<World.Enviro, Texture2D>();
            Environments.Add(World.Enviro.Gym, content.Load<Texture2D>("Images/Environments/gym"));
            Environments.Add(World.Enviro.Street, content.Load<Texture2D>("Images/Environments/street"));
            Environments.Add(World.Enviro.Field, content.Load<Texture2D>("Images/Environments/field"));
            Environments.Add(World.Enviro.Playground, content.Load<Texture2D>("Images/Environments/playground"));

            // Load GameChars
            GameChars = new Dictionary<GameChar.Avatar, Texture2D>();
            GameChars.Add(GameChar.Avatar.Joey, content.Load<Texture2D>("Images/GameChars/joey"));
            GameChars.Add(GameChar.Avatar.Richard, content.Load<Texture2D>("Images/GameChars/richard"));
            GameChars.Add(GameChar.Avatar.Max, content.Load<Texture2D>("Images/GameChars/max"));
            GameChars.Add(GameChar.Avatar.Eduardo, content.Load<Texture2D>("Images/GameChars/eduardo"));
            GameChars.Add(GameChar.Avatar.Dylan, content.Load<Texture2D>("Images/GameChars/dylan"));
            GameChars.Add(GameChar.Avatar.Tim, content.Load<Texture2D>("Images/GameChars/tim"));
            GameChars.Add(GameChar.Avatar.Emily, content.Load<Texture2D>("Images/GameChars/emily"));
            GameChars.Add(GameChar.Avatar.Li, content.Load<Texture2D>("Images/GameChars/li"));
            GameChars.Add(GameChar.Avatar.Omega, content.Load<Texture2D>("Images/GameChars/omega"));

            BallAlive = content.Load<Texture2D>("Images/ballAlive");
            BallDead = content.Load<Texture2D>("Images/ballDead");

            CursorRed = content.Load<Texture2D>("Images/cursorRed");
            CursorGray = content.Load<Texture2D>("Images/cursorGray");

            ReadyText = content.Load<Texture2D>("Images/UI/readyText");
            WinnerTextRed = content.Load<Texture2D>("Images/UI/winnerTextRed");
            WinnerTextWhite = content.Load<Texture2D>("Images/UI/winnerTextWhite");
            LoserTextRed = content.Load<Texture2D>("Images/UI/loserTextRed");
            LoserTextWhite = content.Load<Texture2D>("Images/UI/loserTextWhite");
            GameOverTextRed = content.Load<Texture2D>("Images/UI/gameOverTextRed");
            GameOverTextWhite = content.Load<Texture2D>("Images/UI/gameOverTextWhite");

            PauseMenu = new Texture2D[4];
            PauseMenu[0] = content.Load<Texture2D>("Images/UI/pauseMenuOption0");
            PauseMenu[1] = content.Load<Texture2D>("Images/UI/pauseMenuOption1");
            PauseMenu[2] = content.Load<Texture2D>("Images/UI/pauseMenuOption2");
            PauseMenu[3] = content.Load<Texture2D>("Images/UI/pauseMenuOption3");
        }
    }
}
