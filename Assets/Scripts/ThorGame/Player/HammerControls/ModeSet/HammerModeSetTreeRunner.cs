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
            if (Time.timeScale == 0) return;
            tree.Tick(hammer);
        }
    }
}