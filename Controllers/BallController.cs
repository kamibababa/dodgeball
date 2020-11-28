using Dodgeball.Models;
using Microsoft.Xna.Framework;
using System;

namespace Dodgeball.Controllers
{
    class BallController : EntityController
    {
        private const float Deceleration = 50.0f;
        private const int BouncesToKill = 3;
        private const float RecoilDist = 100.0f;
        private const int Damage = 10;

        public BallController(World world) : base(world)
        {
        }

        public override void Update(float dt)
        {
            // Iterate backwards to allow for deletion
            for (int i = world.Balls.Count - 1; i >= 0; i--)
            {
                Ball ball = world.Balls[i];
                handleBounces(ball);
                setVelocity(ball, dt);
                setPosition(ball, dt);
                ball.SetBounds();
                boundsCheck(ball);
                if (collisionDetect(ball))
                    world.Balls.RemoveAt(i);
            }
        }

        // Checks if balls should be killed after too many bounces
        private void handleBounces(Ball ball)
        {
            if (ball.Bounces >= BouncesToKill)
                ball.IsAlive = false;
        }

        // Handle collisions between ball and GameChars
        // Returns true if ball should be removed
        private bool collisionDetect(Ball ball)
        {
            foreach (GameChar gameChar in world.AllGameChars)
            {
                if (ball.Bounds.Intersects(gameChar.Bounds))
                {
                    // Take damage from alive balls
                    if (ball.IsAlive)
                    {
                        gameChar.Health -= 10;
                        ball.IsAlive = false;

                        // Recoil gameChar
                        Vector2 recoil = new Vector2(ball.Velocity.X, ball.Velocity.Y);
                        if (recoil.LengthSquared() > 0)
                            recoil = Vector2.Multiply(Vector2.Normalize(recoil), RecoilDist);
                        gameChar.Position = Vector2.Add(gameChar.Position, recoil);
                        gameChar.SetBounds();

                        // Reverse ball direction
                        if (ball.Position.X < gameChar.Bounds.X || 
                            ball.Position.X > gameChar.Bounds.X + gameChar.Bounds.Width)
                        {
                            ball.Velocity.X *= -1;
                        }
                        if (ball.Position.Y < gameChar.Bounds.Y ||
                            ball.Position.Y > gameChar.Bounds.Y + gameChar.Bounds.Height)
                        {
                            ball.Velocity.Y *= -1;
                        }
                    }

                    // Pickup dead balls
                    else
                    {
                        if (gameChar.BallsHeld < gameChar.MaxBallsHeld)
                        {
                            gameChar.BallsHeld++;
                            return true; // Immediate return prevents multiple gameChars picking up the same ball on a single frame
                        }
                    }
                }
            }
            return false;
        }

        protected override void boundsCheck(Entity entity)
        {
            // Left wall
            if (entity.Bounds.X < 0)
            {
                entity.Bounds.X = 0;
                entity.Position.X = entity.Bounds.X + entity.Bounds.Width / 2;
                entity.Velocity.X *= -1;
                ((Ball)entity).Bounces++;
            }

            // Right wall
            else if (entity.Bounds.X + entity.Bounds.Width > World.Width)
            {
                entity.Bounds.X = World.Width - entity.Bounds.Width;
                entity.Position.X = entity.Bounds.X + entity.Bounds.Width / 2;
                entity.Velocity.X *= -1;
                ((Ball)entity).Bounces++;
            }

            // Bottom wall
            if (entity.Bounds.Y < 0)
            {
                entity.Bounds.Y = 0;
                entity.Position.Y = entity.Bounds.Y + entity.Bounds.Height / 2;
                entity.Velocity.Y *= -1;
                ((Ball)entity).Bounces++;
            }

            // Top wall
            else if (entity.Bounds.Y + entity.Bounds.Height > World.Height)
            {
                entity.Bounds.Y = World.Height - entity.Bounds.Height;
                entity.Position.Y = entity.Bounds.Y + entity.Bounds.Height / 2;
                entity.Velocity.Y *= -1;
                ((Ball)entity).Bounces++;
            }
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            entity.TopSpeed -= Deceleration * dt;
            if (entity.TopSpeed < 0)
                entity.TopSpeed = 0;
            if (entity.Velocity.LengthSquared() > 0)
                entity.Velocity = Vector2.Normalize(entity.Velocity);
            entity.Velocity = Vector2.Multiply(entity.Velocity, entity.TopSpeed);
        }
    }
}
