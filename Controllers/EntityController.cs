using Dodgeball.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    abstract class EntityController
    {
        protected World world;

        public EntityController(World world)
        {
            this.world = world;
        }

        public abstract void Update(float dt);
        protected abstract void setVelocity(Entity entity, float dt);
        protected abstract void boundsCheck(Entity entity);

        // Add velocity * dt to position
        protected void setPosition(Entity entity, float dt)
        {
            entity.Position = Vector2.Add(entity.Position, Vector2.Multiply(entity.Velocity, dt));
        }
    }
}
