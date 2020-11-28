using Dodgeball.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    class EnemyController : GameCharController
    {
        private enum AI
        {
            Dodge, Pickup, Attack, Avoid
        }

        private AI ai;

        public EnemyController(World world) : base(world)
        {
        }

        public override void Update(float dt)
        {
            foreach (GameChar enemy in world.Enemies)
            {
                ai = setAI(enemy);
                handleAttack(enemy, dt);
                setVelocity(enemy, dt);
                setPosition(enemy, dt);
                enemy.SetBounds();
                boundsCheck(enemy);
            }
        }

        private AI setAI(GameChar enemy)
        {
            bool nearbyDeadBall = false;
            foreach (Ball ball in world.Balls)
            {
                // Dodge if incoming alive ball
                // Presumes enemy is on Right team
                if (ball.Velocity.X > 0 && ball.IsAlive)
                    return AI.Dodge;

                // Pickup if nearby dead ball
                else if (ball.Bounds.Intersects(world.SideBounds[enemy.Side])
                    && !ball.IsAlive)
                {
                    nearbyDeadBall = true;
                }
            }
            if (nearbyDeadBall)
                return AI.Pickup;

            // Attack if holding a ball
            else if (enemy.BallsHeld > 0)
                return AI.Attack;

            // Else, avoid opponent
            return AI.Avoid;
        }

        protected override void handleAttack(GameChar gameChar, float dt)
        {
            // TODO
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            switch (ai)
            {
                case AI.Dodge:
                    dodgeMove(entity);
                    break;
                case AI.Pickup:
                    pickupMove(entity);
                    break;
                case AI.Attack:
                    attackMove(entity);
                    break;
                case AI.Avoid:
                    avoidMove(entity);
                    break;
            }
        }

        private void dodgeMove(Entity entity)
        {
            // Find farthest along alive ball
            Ball oncomingBall = null;
            float farthestX = 0;
            foreach (Ball ball in world.Balls)
            {
                if (ball.IsAlive &&
                    ball.Velocity.X > 0 &&
                    ball.Position.X > farthestX)
                {
                    oncomingBall = ball;
                    farthestX = oncomingBall.Position.X;
                }
            }

            entity.Velocity = new Vector2();
            if (oncomingBall != null)
            {
                entity.Velocity.X = 1;
                // Determines which vertical half of the map the ball will be at and moves away from it
                float t = (entity.Position.X - oncomingBall.Position.X) / oncomingBall.Velocity.X;
                float futureY = oncomingBall.Position.Y + oncomingBall.Velocity.Y * t;
                if (futureY < World.Height / 2) // Ball will be in bottom half
                    entity.Velocity.Y = 1;
                else // Ball will be in top half
                    entity.Velocity.Y = -1;
                entity.Velocity = Vector2.Multiply(Vector2.Normalize(entity.Velocity), entity.TopSpeed);
            }
        }

        private void pickupMove(Entity entity)
        {
            // Find nearest dead ball
            Ball nearestBall = null;
            float shortestDist = Single.MaxValue;
            foreach (Ball ball in world.Balls)
            {
                if (!ball.IsAlive &&
                    Vector2.DistanceSquared(ball.Position, entity.Position) < shortestDist)
                {
                    nearestBall = ball;
                    shortestDist = Vector2.DistanceSquared(ball.Position, entity.Position);
                }
            }

            entity.Velocity = new Vector2();
            if (nearestBall != null)
            {
                entity.Velocity = Vector2.Subtract(nearestBall.Position, entity.Position);
                if (entity.Velocity.LengthSquared() > 0)
                    entity.Velocity = Vector2.Multiply(Vector2.Normalize(entity.Velocity), entity.TopSpeed);
            }
        }

        private void attackMove(Entity entity)
        {
            // TODO
        }

        private void avoidMove(Entity entity)
        {
            // TODO
        }
    }
}
