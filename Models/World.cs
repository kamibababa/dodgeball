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

        public int Width = 256;
        public int Height = 144;

        public Day DayOfWeek;
        public Enviro Environment;

        public World(Day day)
        {
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
        }
    }
}
