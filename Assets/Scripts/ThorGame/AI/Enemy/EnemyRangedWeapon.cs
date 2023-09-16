using UnityEditor;
using UnityEngine;

namespace ThorGame.AI.Enemy
{
    [RequireComponent(typeof(EnemyAmmo))]
    public class EnemyRangedWeapon : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float attackRange;
        [SerializeField] private Transform projectileOrigin;
        
        public EnemyAmmo Ammo { get; private set; }
        private void Awake()
        {
            Ammo = GetComponent<EnemyAmmo>();
        }

        public bool CanShoot(Vector3 position) => Ammo.HasAmmoReady && IsInRange(position);

        public bool IsInRange(Vector3 position)
        {
            float sqrDist = (position - transform.position).sqrMagnitude;
            return sqrDist <= attackRange * attackRange;
        }
        
        public void Shoot(Vector3 target)
        {
            if (!CanShoot(target))
            {
                Debug.LogError("Tried to shoot when CanShoot is false!", gameObject);
                return;
            }
            
            Ammo.ConsumeAmmo();
            Vector3 origin = projectileOrigin ? projectileOrigin.transform.position : transform.position;
            Vector3 direction = (target - origin).normalized;
            Projectile.Shoot(projectilePrefab, transform.position, direction);
        }
        
#if UNITY_EDITOR
        [CustomEditor(typeof(EnemyRangedWeapon))]
        private class Editor : UnityEditor.Editor
        {
            private void OnSceneGUI()
            {
                var ranged = target as EnemyRangedWeapon;
                Handles.color = Color.red;
                Handles.DrawWireDisc(ranged.transform.position, Vector3.forward, ranged.attackRange);
            }
        }
#endif
    }
}