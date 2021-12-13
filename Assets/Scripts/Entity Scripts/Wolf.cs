using UnityEngine;

namespace Entity_Scripts
{
    public class Wolf : BehaviorAgent
    {
        private bool biteTimeout = false;
        private int timeoutCounter = 0;
        public const int damageAmount = 10;
        public const int maximumTimeoutCounter = 300;

        public override void Move(Vector3 velocity, Vector3 direction)
        {
            if (timeoutCounter == maximumTimeoutCounter)
            {
                biteTimeout = false;
                timeoutCounter = 0;
            }

            if (!biteTimeout)
            {
                base.Move(velocity, direction);
            }
            else
            {
                timeoutCounter++;
            }
        }

        public override void Attack(BehaviorAgent target)
        {
            base.Attack(target);

            if (!biteTimeout)
            {
                target.TakeDamage(damageAmount);
                biteTimeout = true;
            }
        }
    }
}
