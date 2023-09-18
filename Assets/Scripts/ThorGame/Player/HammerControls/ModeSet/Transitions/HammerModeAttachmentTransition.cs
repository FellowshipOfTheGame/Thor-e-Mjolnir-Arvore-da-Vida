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

        public override HammerModeNodeTransition Clone(HammerModeNode fromClone, HammerModeNode toClone)
        {
            var clone = (HammerModeAttachmentTransition)base.Clone(fromClone, toClone);
            clone.attachment = attachment;
            return clone;
        }
    }
}