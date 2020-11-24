using Dodgeball.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    class EnemyController : GameCharController
    {
        public EnemyController(World world) : base(world)
        {
        }

        public override void Update(float dt)
        {
            foreach (GameChar enemy in world.Enemies)
            {
                setVelocity(enemy, dt);
                setPosition(enemy, dt);
                enemy.SetBounds();
                boundsCheck(enemy);
            }
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
