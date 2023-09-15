using UnityEngine;

namespace ThorGame.AI.Enemy
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        public abstract bool CanAttack(EntityHealth health);

        protected abstract void PrepareAttack(EntityHealth health);
        public bool TryPrepareAttack(EntityHealth health)
        {
            if (CanAttack(health)) return true;
            PrepareAttack(health);
            return CanAttack(health);
        }

        protected abstract void DoAttack(EntityHealth health);
        public void Attack(EntityHealth health)
        {
            if (!CanAttack(health))
            {
                Debug.LogError("Tried to attack without being able to!", gameObject);
                return;
            }
            DoAttack(health);
        }
    }
}