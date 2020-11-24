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
        private BallController ballController;

        public ControllerSet(World world)
        {
            playerController = new PlayerController(world);
            ballController = new BallController(world);
        }

        public void Update(float dt)
        {
            playerController.Update(dt);
            ballController.Update(dt);
        }
    }
}
