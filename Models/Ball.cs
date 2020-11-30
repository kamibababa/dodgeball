using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Models
{
    class Ball : Entity
    {
        public bool IsAlive;
        public int Bounces;

        private const int NormalSpeed = 600;
        private const int FastSpeed = 800;

        public Ball(Vector2 position, Vector2 direction, bool alive, bool fastThrow)
        {
            // Class attributes
            Bounds.Width = 32;
            Bounds.Height = 32;
            TopSpeed = fastThrow ? FastSpeed :NormalSpeed;

            IsAlive = alive;
            Bounces = 0;

            Position.X = position.X;
            Position.Y = position.Y;

            Velocity = Vector2.Multiply(direction, TopSpeed);

            SetBounds();
        }
    }
}
