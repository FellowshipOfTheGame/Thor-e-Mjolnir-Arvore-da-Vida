using System.Collections;
using UnityEngine;

namespace ThorGame.AI.Enemy
{
    public class EnemyAmmo : MonoBehaviour
    {
        [SerializeField] private Timer reloadTimer;

        public bool HasAmmoReady { get; private set; } = true;
        public bool IsReloading => _reloadCoroutineInstance != null;
        
        private Coroutine _reloadCoroutineInstance;
        public void StartReload()
        {
            if (!HasAmmoReady && !IsReloading)
            {
                _reloadCoroutineInstance = StartCoroutine(ReloadCoroutine());
            }
        }

        public void CancelReload()
        {
            if (!IsReloading) return;
            StopCoroutine(_reloadCoroutineInstance);
            _reloadCoroutineInstance = null;
            reloadTimer.Reset();
        }

        private IEnumerator ReloadCoroutine()
        {
            yield return new WaitUntil(reloadTimer.Tick);
            HasAmmoReady = true;
            _reloadCoroutineInstance = null;
            reloadTimer.Reset();
        }

        public void ConsumeAmmo() => HasAmmoReady = false;
    }
}