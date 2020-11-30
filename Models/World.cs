using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Models
{
    class World
    {
        public enum Day
        {
            Mon, Tue, Wed, Thu, Fri
        }

        public enum Enviro
        {
            Gym, Street, Field, Playground
        }

        public const int Width = 1024;
        public const int Height = 576;

        private const int BallsToSpawn = 3;

        public Day DayOfWeek;
        public Enviro Environment;
        public Dictionary<GameChar.Team, Rectangle> SideBounds;
        public GameChar Player;
        public List<GameChar> AllGameChars;
        public List<GameChar> Enemies;
        public List<Ball> Balls;

        public World(Day day)
        {
            DayOfWeek = day;

            SideBounds = new Dictionary<GameChar.Team, Rectangle>();
            SideBounds.Add(GameChar.Team.Left, new Rectangle(0, 0, Width / 2, Height));
            SideBounds.Add(GameChar.Team.Right, new Rectangle(Width / 2, 0, Width / 2, Height));

            // Set environment
            switch (day)
            {
                case Day.Mon:
                case Day.Fri:
                    Environment = Enviro.Gym;
                    break;
                case Day.Tue:
                    Environment = Enviro.Street;
                    break;
                case Day.Wed:
                    Environment = Enviro.Field;
                    break;
                case Day.Thu:
                    Environment = Enviro.Playground;
                    break;
            }

            // Load GameChars
            AllGameChars = new List<GameChar>();
            Player = new GameChar(GameChar.Team.Left, GameChar.Avatar.Joey, this);
            AllGameChars.Add(Player);
            Enemies = new List<GameChar>();
            GameChar enemy = new GameChar(GameChar.Team.Right, GameChar.Avatar.Max, this);
            Enemies.Add(enemy);
            AllGameChars.Add(enemy);

            // Load balls
            Balls = new List<Ball>();
            for (int i = 0; i < BallsToSpawn; i++)
            {
                Balls.Add(new Ball(
                    new Vector2(Width / 2, Height / (2 * BallsToSpawn) * (1 + 2 * i)),
                    new Vector2(),
                    false,
                    false));
            }

        }
    }
}
