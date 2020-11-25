using Dodgeball.Models;

namespace Dodgeball.Controllers
{
    class BallController : EntityController
    {
        public BallController(World world) : base(world)
        {
        }

        public override void Update(float dt)
        {
            // Iterate backwards to allow for deletion
            for (int i = world.Balls.Count - 1; i >= 0; i--)
            {
                Ball ball = world.Balls[i];
                setVelocity(ball, dt);
                setPosition(ball, dt);
                boundsCheck(ball);
                if (collisionDetect(ball))
                    world.Balls.RemoveAt(i);
            }
        }

        // Handle collisions between ball and GameChars
        // Returns true if ball should be removed
        private bool collisionDetect(Ball ball)
        {
            foreach (GameChar gameChar in world.AllGameChars)
            {
                if (ball.Bounds.Intersects(gameChar.Bounds))
                {
                    // TODO Take damage from alive balls
                    if (ball.IsAlive)
                    {

                    }
                    // Pickup dead balls
                    else
                    {
                        if (gameChar.BallsHeld < gameChar.MaxBallsHeld)
                        {
                            gameChar.BallsHeld++;
                            return true; // Immediate return prevents multiple gameChars picking up the same ball on a single frame
                        }
                    }
                }
            }
            return false;
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
