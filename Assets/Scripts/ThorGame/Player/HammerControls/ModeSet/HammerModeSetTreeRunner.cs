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
    }
}