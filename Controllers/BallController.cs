using Dodgeball.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    class BallController : EntityController
    {
        public BallController(World world) : base(world)
        {
        }

        public override void Update(float dt)
        {
            foreach (Ball ball in world.Balls)
            {
                setVelocity(ball, dt);
                setPosition(ball, dt);
                boundsCheck(ball);
                collisionDetect(ball);
            }
        }

        // Handle collisions between ball and GameChars
        private void collisionDetect(Ball ball)
        {
            // TODO
        }

        protected override void boundsCheck(Entity entity)
        {
            // TODO
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            // TODO
        }
    }
}
