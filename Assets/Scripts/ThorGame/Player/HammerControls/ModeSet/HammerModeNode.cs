using System.Collections.Generic;
using System.Linq;
using ThorGame.Player.HammerControls.Modes;
using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    public class HammerModeNode : ScriptableObject, ITypedNode<Hammer, HammerModeNode, HammerModeNode>, ICloneable<HammerModeNode>
    {
        [SerializeField] private List<HammerMode> modes = new();
        [SerializeField] private List<HammerModeNodeTransition> transitions = new();

        public IEnumerable<ITypedTransition<Hammer, HammerModeNode, HammerModeNode, HammerModeNode>> Transitions =>
            (IEnumerable<ITypedTransition<Hammer, HammerModeNode, HammerModeNode, HammerModeNode>>)transitions;

        public HammerMode ApplicableMode(Hammer data) => modes.FirstOrDefault(data.IsUnlocked);

        private HammerMode _currentMode;
        public void Begin(Hammer data)
        {
            _currentMode = ApplicableMode(data);
            _currentMode.Begin(data);
        }
        
        public HammerModeNode Tick(Hammer data)
        {
            foreach (var transition in transitions)
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

        public HammerModeNode Clone()
        {
            return Instantiate(this);
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
            transitions.Add(trans);
        }
    }
}