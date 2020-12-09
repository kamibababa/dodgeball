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
            Gym, Street, Playground, Field
        }

        public const int Width = 1024;
        public const int Height = 576;
        public static int NumDays = Enum.GetValues(typeof(Day)).Length;

        private const int BallsToSpawn = 3;
        private const int BallsToSpawnOnFriday = 5;

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
                    Environment = Enviro.Playground;
                    break;
                case Day.Thu:
                    Environment = Enviro.Field;
                    break;
            }

            // Load player
            AllGameChars = new List<GameChar>();
            Player = new GameChar(GameChar.Team.Left, GameChar.Avatar.Joey, this, 0, 1);
            AllGameChars.Add(Player);

            // Load enemies
            Enemies = new List<GameChar>();
            switch (day)
            {
                case Day.Mon:
                    loadEnemy(GameChar.Avatar.Richard, 0, 1);
                    break;
                case Day.Tue:
                    loadEnemy(GameChar.Avatar.Max, 0, 1);
                    break;
                case Day.Wed:
                    loadEnemy(GameChar.Avatar.Eduardo, 0, 2);
                    loadEnemy(GameChar.Avatar.Dylan, 1, 2);
                    break;
                case Day.Thu:
                    loadEnemy(GameChar.Avatar.Tim, 0, 3);
                    loadEnemy(GameChar.Avatar.Emily, 1, 3);
                    loadEnemy(GameChar.Avatar.Li, 2, 3);
                    break;
                case Day.Fri:
                    loadEnemy(GameChar.Avatar.Omega, 0, 1);
                    break;
            }

            // Load balls
            Balls = new List<Ball>();
            int numBalls = day == Day.Fri ? BallsToSpawnOnFriday : BallsToSpawn;
            for (int i = 0; i < numBalls; i++)
            {
                Balls.Add(new Ball(
                    new Vector2(Width / 2, Height / (2 * numBalls) * (1 + 2 * i)),
                    new Vector2(),
                    false,
                    false));
            }

        }

        private void loadEnemy(GameChar.Avatar avatar, int charNum, int totalChars)
        {
            GameChar enemy = new GameChar(GameChar.Team.Right, avatar, this, charNum, totalChars);
            Enemies.Add(enemy);
            AllGameChars.Add(enemy);
        }
    }
}
