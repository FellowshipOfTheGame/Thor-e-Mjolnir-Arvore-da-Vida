using ThorGame.Player.HammerControls.Modes;
using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    public class HammerModeSetTreeRunner : TreeRunner<HammerModeSetTree, Hammer, HammerModeNode, HammerModeNode>
    {
        [SerializeField] private SlamHammerMode slam;
        [SerializeField] private PrepareThrowHammerMode prepare;
        [SerializeField] private ThrownHammerMode thrown;
        
        protected override void Awake()
        {
            tree = HammerModeSetTree.DEBUG_INSTANCE(slam, prepare, thrown);
            base.Awake();
        }
    }
}