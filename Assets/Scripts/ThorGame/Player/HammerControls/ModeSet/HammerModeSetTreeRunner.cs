using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    public class HammerModeSetTreeRunner : MonoBehaviour
    {
        [SerializeField] private HammerModeSetTree tree;
        [SerializeField] private Hammer hammer;
        
        private void Awake()
        {
            tree = tree.Clone();
        }

        private void Update()
        {
            tree.Tick(hammer);
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IHittable hittable))
            {
                tree.OnCollide(hammer, hittable);
            }
        }
    }
}