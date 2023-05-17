using UnityEngine;

namespace ThorGame.Player.HammerControls.Modes
{
    [CreateAssetMenu(menuName = "HammerModes/PrepareThrow", fileName = "PrepareThrowHammerMode", order = 0)]
    public class PrepareThrowHammerMode : HammerMode
    {
        public override Hammer.Attachment AttachmentMode => Hammer.Attachment.Strap;
        
        protected override void OnBegin(Hammer hammer)
        {
        }

        public override void End(Hammer hammer)
        {
        }

        public override void Tick(Hammer hammer)
        {
        }
    }
}