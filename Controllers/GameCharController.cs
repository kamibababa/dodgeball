using Dodgeball.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    abstract class GameCharController : EntityController
    {
        protected GameCharController(World world) : base(world)
        {
        }

        protected abstract void handleAttack(GameChar gameChar, float dt);

        protected override void boundsCheck(Entity entity)
        {
            // TODO
        }

        protected void throwBall(GameChar thrower, Vector2 throwHere)
        {
            // TODO
        }
    }
}
