using UnityEngine;

namespace ThorGame.Player.HammerControls.Modes
{
    public abstract class HammerMode : ScriptableObject
    {
        public abstract Hammer.Attachment AttachmentMode { get; }

        public void Begin(Hammer hammer)
        {
            hammer.AttachmentMode = AttachmentMode;
            OnBegin(hammer);
        }
        protected abstract void OnBegin(Hammer hammer);
        public abstract void End(Hammer hammer);
        public abstract void Tick(Hammer hammer);
        
        public void OnCollide(Hammer hammer, IHittable target)
        {
            target.Hit(hammer.Rigidbody.position, hammer.Rigidbody.velocity);
        }
    }
}