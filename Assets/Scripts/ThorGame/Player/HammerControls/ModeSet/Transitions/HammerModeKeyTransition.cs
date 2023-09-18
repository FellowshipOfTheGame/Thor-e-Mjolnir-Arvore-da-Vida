using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet.Transitions
{
    public class HammerModeKeyTransition : HammerModeNodeTransition
    {
        [SerializeField] private InputCondition input;

        protected override bool TransitionCondition(Hammer hammer) => input;

        public override HammerModeNodeTransition Clone(HammerModeNode fromClone, HammerModeNode toClone)
        {
            var clone = (HammerModeKeyTransition)base.Clone(fromClone, toClone);
            clone.input = input;
            return clone;
        }
    }
}