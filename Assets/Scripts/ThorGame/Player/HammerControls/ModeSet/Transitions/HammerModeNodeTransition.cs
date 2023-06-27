using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet.Transitions
{
    public abstract class HammerModeNodeTransition : TypedConnection<HammerModeNode, HammerModeNodeTransition>
    {
        [SerializeField] private bool negateCondition;

        protected abstract bool TransitionCondition(Hammer hammer);
        
        public bool Condition(Hammer hammer)
        {
            return To.ApplicableMode(hammer) != null &&
                   (negateCondition ? !TransitionCondition(hammer) : TransitionCondition(hammer));
        }
    }
}