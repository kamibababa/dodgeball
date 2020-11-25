using Dodgeball.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    class PlayerController : GameCharController
    {
        public PlayerController(World world) : base(world)
        {
        }

        public override void Update(float dt)
        {
            handleAttack(world.Player, dt);
            setVelocity(world.Player, dt);
            setPosition(world.Player, dt);
            world.Player.SetBounds();
            boundsCheck(world.Player);
        }

        protected override void handleAttack(GameChar gameChar, float dt)
        {
            if (Input.Throw)
            {
                Input.Throw = false;
                if (gameChar.BallsHeld > 0)
                {
                    gameChar.BallsHeld--;
                    throwBall(gameChar, Input.ThrowHere);
                }
            }
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            entity.Velocity = new Vector2();
            if (Input.MoveLeft)
                entity.Velocity.X -= 1;
            if (Input.MoveRight)
                entity.Velocity.X += 1;
            if (Input.MoveUp)
                entity.Velocity.Y -= 1;
            if (Input.MoveDown)
                entity.Velocity.Y += 1;
            if (entity.Velocity.LengthSquared() > 0)
                entity.Velocity = Vector2.Multiply(Vector2.Normalize(entity.Velocity), entity.TopSpeed);
        }
    }
}
