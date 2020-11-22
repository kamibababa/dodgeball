using Dodgeball.Models;
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
            setVelocity(world.Player, dt);
            setPosition(world.Player, dt);
            world.Player.SetBounds();
            boundsCheck(world.Player);
        }

        protected override void handleAttack(GameChar gameChar, float dt)
        {
            // TODO
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            // TODO
        }
    }
}
