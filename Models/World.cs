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

        public int Width = 1024;
        public int Height = 576;

        public Day DayOfWeek;
        public Enviro Environment;
        public GameChar Player;
        public List<GameChar> AllGameChars;
        public List<GameChar> Enemies;

        public World(Day day)
        {
            DayOfWeek = day;

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
        }
    }
}
