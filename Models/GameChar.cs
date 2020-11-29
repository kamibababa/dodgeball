using Microsoft.Xna.Framework;
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

        public const float LungeLength = 0.15f;
        public const float LungeCooldownLength = 3.0f;
        public const float LungeSpeed = 1200;
        public const float EnemyAttackWaitMax = 3.0f;

        private const int OffWall = 40;

        public Team Side;
        public Avatar AvatarType;
        public int Health, MaxHealth;
        public int BallsHeld, MaxBallsHeld;
        public float LungeTimer, LungeCooldown;
        public Vector2 LungeDirection;
        public float AttackTimer; // Only used by enemy AI

        public GameChar(Team team, Avatar avatar, World world)
        {
            // Class attributes
            Bounds.Width = 36;
            Bounds.Height = 80;
            TopSpeed = 220;
            MaxHealth = 100;
            MaxBallsHeld = 3;

            Side = team;
            AvatarType = avatar;
            Health = MaxHealth;
            BallsHeld = 0;

            Random random = new Random();
            AttackTimer = (float) random.NextDouble() * EnemyAttackWaitMax;

            // Set position
            if (Side == Team.Left)
                Position.X = OffWall;
            else // Right
                Position.X = World.Width - OffWall;
            Position.Y = World.Height / 2;

            SetBounds();
        }
    }
}
