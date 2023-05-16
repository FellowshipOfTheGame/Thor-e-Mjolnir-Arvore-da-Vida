using UnityEngine;

namespace ThorGame.Player.HammerControls.Modes
{
    [CreateAssetMenu(menuName = "HammerModes/PrepareThrow", fileName = "PrepareThrowHammerMode", order = 0)]
    public class PrepareThrowHammerMode : HammerMode
    {
        public override void Begin(Hammer hammer)
        {
            hammer.SetStrap(true);
        }

        public override void End(Hammer hammer)
        {
            hammer.SetStrap(false);
        }

        public override void Tick(Hammer hammer)
        {
        }
    }
}