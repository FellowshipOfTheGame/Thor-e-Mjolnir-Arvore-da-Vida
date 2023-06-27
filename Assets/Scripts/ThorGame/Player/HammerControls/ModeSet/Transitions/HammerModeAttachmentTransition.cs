using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet.Transitions
{
    public class HammerModeAttachmentTransition : HammerModeNodeTransition
    {
        [SerializeField] private Hammer.Attachment attachment;

        protected override bool TransitionCondition(Hammer hammer)
        {
            return hammer.AttachmentMode == attachment;
        }
        
        public override HammerModeNodeTransition Clone()
        {
            var clone = (HammerModeAttachmentTransition)base.Clone();
            clone.attachment = attachment;
            return clone;
        }
    }
}