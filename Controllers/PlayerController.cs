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
            handleLunge(world.Player, dt);
            setPosition(world.Player, dt);
            world.Player.SetBounds();
            boundsCheck(world.Player);
        }

        private void handleLunge(GameChar player, float dt)
        {
            // Update timers
            player.LungeTimer -= dt;
            if (player.LungeTimer < 0)
                player.LungeTimer = 0;
            player.LungeCooldown -= dt;
            if (player.LungeCooldown < 0)
                player.LungeCooldown = 0;

            // Start lunge
            if (Input.Lunge)
            {
                Input.Lunge = false;
                if (player.LungeCooldown <= 0)
                {
                    player.LungeTimer = GameChar.LungeLength;
                    player.LungeCooldown = GameChar.LungeCooldownLength;
                    player.LungeDirection = Vector2.Subtract(Input.LungeHere, player.Position);
                    if (player.LungeDirection.LengthSquared() > 0) // Prevents divide-by-zero crash if player clicks exactly on gameChar position
                        player.LungeDirection = Vector2.Normalize(player.LungeDirection);
                }
            }

            // Change velocity if active lunge
            if (player.LungeTimer > 0)
            {
                player.Velocity = Vector2.Multiply(player.LungeDirection, GameChar.LungeSpeed);
            }
        }

        protected override void handleAttack(GameChar gameChar, float dt)
        {
            if (Input.Throw)
            {
                Input.Throw = false;
                if (gameChar.BallsHeld > 0)
                {
                    gameChar.BallsHeld--;
                    // Check for fast throw
                    bool fastThrow = false;
                    foreach (GameChar enemy in world.Enemies)
                    {
                        if (enemy.Bounds.Contains(Input.MouseVirtualPos))
                        {
                            fastThrow = true;
                            break;
                        }
                    }
                    throwBall(gameChar, Input.ThrowHere, fastThrow);
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
