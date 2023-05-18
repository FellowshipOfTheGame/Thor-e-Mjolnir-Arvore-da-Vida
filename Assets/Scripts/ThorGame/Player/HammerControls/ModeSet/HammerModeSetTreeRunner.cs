using ThorGame.Player.HammerControls.Modes;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    public class HammerModeSetTreeRunner : MonoBehaviour
    {
        [SerializeField] private HammerModeSetTree tree;
        [SerializeField] private Hammer hammer;
        
        //TODO DEBUG
        [SerializeField] private SlamHammerMode slam;
        [SerializeField] private PrepareThrowHammerMode prepare;
        [SerializeField] private ThrownHammerMode thrown;
        
        private void Awake()
        {
            //TODO DEBUG
            tree = HammerModeSetTree.DEBUG_INSTANCE(slam, prepare, thrown);

            tree = tree.Clone();
        }

        private void Update()
        {
            tree.Tick(hammer);
        }
    }
}