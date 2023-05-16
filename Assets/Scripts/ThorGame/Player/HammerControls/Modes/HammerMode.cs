using UnityEngine;

namespace ThorGame.Player.HammerControls.Modes
{
    public abstract class HammerMode : ScriptableObject
    {
        public abstract void Begin(Hammer hammer);
        public abstract void End(Hammer hammer);
        public abstract void Tick(Hammer hammer);
    }
}