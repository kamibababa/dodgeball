using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Models
{
    class GameChar : Entity
    {
        public enum Team
        {
            Left, Right
        }

        public enum Avatar
        {
            Joey, Richard, Max, Eduardo, Dylan, Tim, Emily, Li, Omega
        }

        private const int OffWall = 40;

        public Team Side;
        public Avatar AvatarType;
        public int Health, MaxHealth;

        public GameChar(Team team, Avatar avatar, World world)
        {
            // Class attributes
            Bounds.Width = 36;
            Bounds.Height = 80;
            TopSpeed = 220;
            MaxHealth = 100;

            Side = team;
            AvatarType = avatar;
            Health = MaxHealth;

            // Set position
            if (Side == Team.Left)
                Position.X = OffWall;
            else // Right
                Position.X = world.Width - OffWall;
            Position.Y = world.Height / 2;

            SetBounds();
        }
    }
}
