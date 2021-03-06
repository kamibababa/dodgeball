﻿using Dodgeball.Models;
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

        private const int AttackFollowRange = 100;
        private const int SpreadApartSpeed = 10;

        private AI ai;
        private Random random;

        public EnemyController(World world) : base(world)
        {
            random = new Random();
        }

        public override void Update(float dt)
        {
            for (int i = world.Enemies.Count - 1; i >= 0; i--)
            {
                GameChar enemy = world.Enemies[i];
                ai = setAI(enemy);
                handleAttack(enemy, dt);
                setVelocity(enemy, dt);
                setPosition(enemy, dt);
                enemy.SetBounds();
                spreadApart(enemy);
                boundsCheck(enemy);

                // Remove dead enemies
                if (enemy.Health <= 0)
                {
                    world.AllGameChars.Remove(enemy);
                    world.Enemies.RemoveAt(i);
                }
            }
        }

        // Resolves problem where enemies can be recoiled on top of one another at the map's edge
        private void spreadApart(GameChar enemy)
        {
            foreach (GameChar otherEnemy in world.Enemies)
            {
                if (!enemy.Equals(otherEnemy)) // Ensure enemy is not referencing itself
                {
                    if (enemy.Bounds.Intersects(otherEnemy.Bounds))
                    {
                        // Enemy is left of other enemy
                        if (enemy.Position.X < otherEnemy.Position.X)
                            enemy.Position.X -= SpreadApartSpeed;
                        // Enemy is right of other enemy
                        else if (enemy.Position.X > otherEnemy.Position.X)
                            enemy.Position.X += SpreadApartSpeed;
                        // Enemy is above other enemy
                        if (enemy.Position.Y < otherEnemy.Position.Y)
                            enemy.Position.Y -= SpreadApartSpeed;
                        // Enemy is below other enemy
                        else if (enemy.Position.Y > otherEnemy.Position.Y)
                            enemy.Position.Y += SpreadApartSpeed;
                        enemy.SetBounds();
                    }
                }
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
            if (ai == AI.Attack)
            {
                gameChar.AttackTimer -= dt;
                if (gameChar.AttackTimer <= 0)
                {
                    gameChar.AttackTimer = (float)random.NextDouble() * GameChar.EnemyAttackWaitMax;
                    if (gameChar.BallsHeld > 0)
                    {
                        gameChar.BallsHeld--;
                        throwBall(gameChar, world.Player.Position, false); // Always a normal throw
                    }
                }
            }
        }

        // Do not allow for collisions between enemies
        protected override void setPosition(Entity entity, float dt)
        {
            Vector2 originalPos = new Vector2(entity.Position.X, entity.Position.Y);
            Rectangle originalBounds = new Rectangle(entity.Bounds.X, entity.Bounds.Y, entity.Bounds.Width, entity.Bounds.Height);
            
            // X
            Vector2 futurePos = new Vector2(originalPos.X, originalPos.Y);
            futurePos.X += entity.Velocity.X * dt;
            Rectangle futureBounds = new Rectangle(originalBounds.X, originalBounds.Y, originalBounds.Width, originalBounds.Height);
            futureBounds.X = (int)(futurePos.X - futureBounds.Width / 2);
            futureBounds.Y = (int)(futurePos.Y - futureBounds.Height / 2);
            // Check for collisions
            bool intersects = false;
            foreach (GameChar enemy in world.Enemies)
            {
                if (!entity.Equals(enemy)) // Ensure enemy is not referencing itself
                {
                    if (futureBounds.Intersects(enemy.Bounds))
                    {
                        intersects = true;
                        break;
                    }
                }
            }
            if (!intersects)
                entity.Position.X += entity.Velocity.X * dt;

            // Y
            futurePos = new Vector2(originalPos.X, originalPos.Y);
            futurePos.Y += entity.Velocity.Y * dt;
            futureBounds = new Rectangle(originalBounds.X, originalBounds.Y, originalBounds.Width, originalBounds.Height);
            futureBounds.X = (int)(futurePos.X - futureBounds.Width / 2);
            futureBounds.Y = (int)(futurePos.Y - futureBounds.Height / 2);
            // Check for collisions
            intersects = false;
            foreach (GameChar enemy in world.Enemies)
            {
                if (!entity.Equals(enemy)) // Ensure enemy is not referencing itself
                {
                    if (futureBounds.Intersects(enemy.Bounds))
                    {
                        intersects = true;
                        break;
                    }
                }
            }
            if (!intersects)
                entity.Position.Y += entity.Velocity.Y * dt;
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
            entity.Velocity = new Vector2();

            // Move forward if player has no balls
            if (world.Player.BallsHeld == 0)
                entity.Velocity.X = -1;

            // Follow player
            // Player is below enemy
            if (world.Player.Position.Y - entity.Position.Y >= AttackFollowRange)
                entity.Velocity.Y = 1;
            // Player is above enemy
            else if (world.Player.Position.Y - entity.Position.Y <= -AttackFollowRange)
                entity.Velocity.Y = -1;

            if (entity.Velocity.LengthSquared() > 0)
                entity.Velocity = Vector2.Multiply(Vector2.Normalize(entity.Velocity), entity.TopSpeed);
        }

        private void avoidMove(Entity entity)
        {
            entity.Velocity = new Vector2();

            entity.Velocity.X = 1;

            // Move down if player is in top third of screen
            if (world.Player.Position.Y < World.Height / 3)
                entity.Velocity.Y = 1;
            // Move up if player is in bottom third of screen
            else if (world.Player.Position.Y > World.Height / 3 * 2)
                entity.Velocity.Y = -1;

            if (entity.Velocity.LengthSquared() > 0)
                entity.Velocity = Vector2.Multiply(Vector2.Normalize(entity.Velocity), entity.TopSpeed);
        }
    }
}
