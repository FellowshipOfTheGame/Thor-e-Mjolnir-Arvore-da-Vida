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

        //TODO DEBUG ERA PRIVATE DEIXEI PROTECTED O FROM E TO
        public static HammerModeKeyTransition DEBUG_INSTANCE(HammerModeNode from, HammerModeNode to, KeyCode key)
        {
            var instance = CreateInstance<HammerModeKeyTransition>();
            instance.from = from;
            instance.to = to;
            instance.key = key;
            return instance;
        }
    }
}