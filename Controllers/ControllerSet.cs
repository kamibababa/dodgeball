using Dodgeball.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    // Container class for all controllers
    class ControllerSet
    {
        private PlayerController playerController;
        private EnemyController enemyController;
        private BallController ballController;

        public ControllerSet(World world)
        {
            playerController = new PlayerController(world);
            enemyController = new EnemyController(world);
            ballController = new BallController(world);
        }

        public void Update(float dt)
        {
            ballController.Update(dt); // Put ahead of GameCharControllers since recoil can push GameChars out of bounds
            playerController.Update(dt);
            enemyController.Update(dt);
        }
    }
}
