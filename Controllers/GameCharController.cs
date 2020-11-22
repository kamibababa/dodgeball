using Dodgeball.Models;
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

        protected override void boundsCheck(Entity entity)
        {
            // TODO
        }
    }
}
