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

        public Ball(Vector2 position, Vector2 direction, bool alive)
        {
            // Class attributes
            Bounds.Width = 32;
            Bounds.Height = 32;
            TopSpeed = 600;

            IsAlive = alive;
            Bounces = 0;

            Position.X = position.X;
            Position.Y = position.Y;

            Velocity = Vector2.Multiply(direction, TopSpeed);

            SetBounds();
        }
    }
}
