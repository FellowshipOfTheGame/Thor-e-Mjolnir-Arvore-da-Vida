using UnityEngine;

namespace ThorGame.Player.HammerControls.Modes
{
    [CreateAssetMenu(menuName = "HammerModes/Thrown", fileName = "ThrownHammerMode", order = 0)]
    public class ThrownHammerMode : HammerMode
    {
        public override Hammer.Attachment AttachmentMode => Hammer.Attachment.Free;
        
        protected override void OnBegin(Hammer hammer)
        {
        }

        public override void End(Hammer hammer)
        {
        }

        public override void Tick(Hammer hammer)
        {
            Vector2 vel = hammer.Rigidbody.velocity;
            if (vel != Vector2.zero)
            {
                hammer.transform.up = vel.normalized;
            }
        }
    }
}