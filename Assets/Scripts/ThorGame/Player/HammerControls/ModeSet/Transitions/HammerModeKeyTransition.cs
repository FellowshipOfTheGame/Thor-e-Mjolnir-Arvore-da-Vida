using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet.Transitions
{
    public class HammerModeKeyTransition : HammerModeNodeTransition
    {
        [SerializeField] private InputCondition input;

        protected override bool TransitionCondition(Hammer hammer) => input;

        public override HammerModeNodeTransition Clone()
        {
            var clone = (HammerModeKeyTransition)base.Clone();
            clone.input = input;
            return clone;
        }
    }
}