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

        private const int OffWall = 10;

        public Team Side;
        public Avatar AvatarType;
        private Team left;

        public GameChar(Team team, Avatar avatar, World world)
        {
            // Class attributes
            Bounds.Width = 9;
            Bounds.Height = 20;
            TopSpeed = 40;

            Side = team;
            AvatarType = avatar;

            // Set position
            if (Side == Team.Left)
                Position.X = OffWall;
            else // Right
                Position.X = world.Width - OffWall;
            Position.Y = world.Height / 2;

            SetBounds();
        }

        public GameChar(Team left)
        {
            this.left = left;
        }
    }
}
