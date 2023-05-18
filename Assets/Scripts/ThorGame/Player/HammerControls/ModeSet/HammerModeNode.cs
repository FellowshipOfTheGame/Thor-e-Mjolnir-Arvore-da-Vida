using System.Collections.Generic;
using System.Linq;
using ThorGame.Player.HammerControls.Modes;
using ThorGame.Player.HammerControls.ModeSet.Transitions;
using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    public class HammerModeNode : TypedNode<HammerModeNode, HammerModeNodeTransition>
    {
        [SerializeField] private List<HammerMode> modes = new();

        public override HammerModeNode Clone()
        {
            var clone = base.Clone();
            clone.modes = new List<HammerMode>(modes);
            return clone;
        }

        public HammerMode ApplicableMode(Hammer data) => modes.FirstOrDefault(data.IsUnlocked);

        private HammerMode _currentMode;
        public void Begin(Hammer data)
        {
            _currentMode = ApplicableMode(data);
            _currentMode.Begin(data);
        }
        
        public HammerModeNode Tick(Hammer data)
        {
            foreach (var transition in Connections)
            {
                if (transition.Condition(data))
                {
                    return transition.To;
                }
            }

            _currentMode.Tick(data);
            return null;
        }

        public void End(Hammer data)
        {
            _currentMode.End(data);
            _currentMode = null;
        }

        //TODO DEBUG ERA ARRAY PASSEI PRA LISTA
        public static HammerModeNode DEBUG_GETINSTANCE(HammerMode mode)
        {
            var instance = CreateInstance<HammerModeNode>();
            instance.modes.Add(mode);
            return instance;
        }
        public void DEBUG_ADDTRANS(HammerModeNodeTransition trans)
        {
            connections.Add(trans);
        }

        public override ConnectionCount InputConnection => ConnectionCount.Multi;
        public override ConnectionCount OutputConnection => ConnectionCount.Multi;
    }
}