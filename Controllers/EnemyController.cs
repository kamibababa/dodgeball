using Dodgeball.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodgeball.Controllers
{
    class EnemyController : GameCharController
    {
        private enum AI
        {
            Dodge, Pickup, Attack, Avoid
        }

        private AI ai;

        public EnemyController(World world) : base(world)
        {
        }

        public override void Update(float dt)
        {
            foreach (GameChar enemy in world.Enemies)
            {
                ai = setAI(enemy);
                handleAttack(enemy, dt);
                setVelocity(enemy, dt);
                setPosition(enemy, dt);
                enemy.SetBounds();
                boundsCheck(enemy);
            }
        }

        private AI setAI(GameChar enemy)
        {
            bool nearbyDeadBall = false;
            foreach (Ball ball in world.Balls)
            {
                // Dodge if incoming alive ball
                // Presumes enemy is on Right team
                if (ball.Velocity.X > 0 && ball.IsAlive)
                    return AI.Dodge;

                // Pickup if nearby dead ball
                else if (ball.Bounds.Intersects(world.SideBounds[enemy.Side])
                    && !ball.IsAlive)
                {
                    nearbyDeadBall = true;
                }
            }
            if (nearbyDeadBall)
                return AI.Pickup;

            // Attack if holding a ball
            else if (enemy.BallsHeld > 0)
                return AI.Attack;

            // Else, avoid opponent
            return AI.Avoid;
        }

        protected override void handleAttack(GameChar gameChar, float dt)
        {
            // TODO
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            switch (ai)
            {
                case AI.Dodge:
                    dodgeMove(entity);
                    break;
                case AI.Pickup:
                    pickupMove(entity);
                    break;
                case AI.Attack:
                    attackMove(entity);
                    break;
                case AI.Avoid:
                    avoidMove(entity);
                    break;
            }
        }

        private void dodgeMove(Entity entity)
        {
            // TODO
        }

        private void pickupMove(Entity entity)
        {
            // TODO
        }

        private void attackMove(Entity entity)
        {
            // TODO
        }

        private void avoidMove(Entity entity)
        {
            // TODO
        }
    }
}
