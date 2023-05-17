using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    public abstract class HammerModeNodeTransition : ScriptableObject, ITypedTransition<Hammer, object, HammerModeNode, HammerModeNode>
    {
        [SerializeField] private bool negate;
        
        [field: SerializeField] [field: HideInInspector]
        public HammerModeNode From { get; protected set; }
        
        [field: SerializeField] [field: HideInInspector]
        public HammerModeNode To { get; protected set; }

        protected abstract bool TransitionCondition(Hammer hammer);
        
        public bool Condition(Hammer hammer)
        {
            return To.ApplicableMode(hammer) != null &&
                   (negate ? !TransitionCondition(hammer) : TransitionCondition(hammer));
        }
    }

    public class HammerModeKeyTransition : HammerModeNodeTransition
    {
        [SerializeField] private KeyCode key;
        
        protected override bool TransitionCondition(Hammer hammer)
        {
            return Input.GetKeyDown(key);
        }
        
        //TODO DEBUG ERA PRIVATE DEIXEI PROTECTED O FROM E TO
        public static HammerModeKeyTransition DEBUG_INSTANCE(HammerModeNode from, HammerModeNode to, KeyCode key)
        {
            var instance = CreateInstance<HammerModeKeyTransition>();
            instance.From = from;
            instance.To = to;
            instance.key = key;
            return instance;
        }
    }

    public class HammerModeAttachmentTransition : HammerModeNodeTransition
    {
        [SerializeField] private Hammer.Attachment attachment;

        protected override bool TransitionCondition(Hammer hammer)
        {
            return hammer.AttachmentMode == attachment;
        }
    }
}