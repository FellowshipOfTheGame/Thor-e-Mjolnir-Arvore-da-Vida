using UnityEngine;

namespace ThorGame.Player.HammerControls.Modes
{
    [CreateAssetMenu(menuName = "HammerModes/Slam", fileName = "SlamHammerMode", order = 0)]
    public class SlamHammerMode : HammerMode
    {
        public override Hammer.Attachment AttachmentMode => Hammer.Attachment.Held;

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