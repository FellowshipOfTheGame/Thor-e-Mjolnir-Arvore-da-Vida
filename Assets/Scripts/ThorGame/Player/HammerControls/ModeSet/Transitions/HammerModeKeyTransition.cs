using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet.Transitions
{
    public class HammerModeKeyTransition : HammerModeNodeTransition
    {
        [SerializeField] private KeyCode key;
        
        protected override bool TransitionCondition(Hammer hammer)
        {
            return Input.GetKeyDown(key);
        }

        public override HammerModeNodeTransition Clone()
        {
            var clone = (HammerModeKeyTransition)base.Clone();
            clone.key = key;
            return clone;
        }
    }
}