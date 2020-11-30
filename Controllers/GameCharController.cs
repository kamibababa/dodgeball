using Dodgeball.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    abstract class GameCharController : EntityController
    {
        protected const int BallSpawnDist = 40;

        protected GameCharController(World world) : base(world)
        {
        }

        protected abstract void handleAttack(GameChar gameChar, float dt);

        // Keeps GameChar bound to correct side
        protected override void boundsCheck(Entity entity)
        {
            Rectangle sideBounds = world.SideBounds[((GameChar)entity).Side];
            
            // Left wall
            if (entity.Bounds.X < sideBounds.X)
            {
                entity.Bounds.X = sideBounds.X;
                entity.Position.X = entity.Bounds.X + entity.Bounds.Width / 2;
            }

            // Right wall
            else if (entity.Bounds.X + entity.Bounds.Width > sideBounds.X + sideBounds.Width)
            {
                entity.Bounds.X = sideBounds.X + sideBounds.Width - entity.Bounds.Width;
                entity.Position.X = entity.Bounds.X + entity.Bounds.Width / 2;
            }

            // Top wall
            if (entity.Bounds.Y < sideBounds.Y)
            {
                entity.Bounds.Y = sideBounds.Y;
                entity.Position.Y = entity.Bounds.Y + entity.Bounds.Height / 2;
            }

            // Bottom wall
            else if (entity.Bounds.Y + entity.Bounds.Height > sideBounds.Y + sideBounds.Height)
            {
                entity.Bounds.Y = sideBounds.Y + sideBounds.Height - entity.Bounds.Height;
                entity.Position.Y = entity.Bounds.Y + entity.Bounds.Height / 2;
            }
        }

        protected void throwBall(GameChar thrower, Vector2 throwHere, bool fastThrow)
        {
            Vector2 pos = new Vector2(thrower.Position.X, thrower.Position.Y);
            if (thrower.Side == GameChar.Team.Left)
                pos.X += BallSpawnDist;
            else // Right
                pos.X -= BallSpawnDist;

            Vector2 dir = Vector2.Subtract(throwHere, pos);
            if (dir.LengthSquared() > 0) // Prevents divide-by-zero crash if player clicks exactly on gameChar position
                dir = Vector2.Normalize(dir);

            world.Balls.Add(new Ball(pos, dir, true, fastThrow));
        }
    }
}
