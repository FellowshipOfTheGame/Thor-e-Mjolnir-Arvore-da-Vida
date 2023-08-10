using UnityEngine;

namespace ThorGame.Player.HammerControls.Modes
{
    [CreateAssetMenu(menuName = "HammerModes/Thrown", fileName = "ThrownHammerMode", order = 0)]
    public class ThrownHammerMode : HammerMode
    {
        [SerializeField] private InputCondition recallInput;
    
        public override Hammer.Attachment AttachmentMode => Hammer.Attachment.Free;
        
        protected override void OnBegin(Hammer hammer)
        {
        }

        public override void End(Hammer hammer)
        {
        }

        public override void Tick(Hammer hammer)
        {
            if (recallInput)
            {
                hammer.Recall();
            }
        }
    }
}