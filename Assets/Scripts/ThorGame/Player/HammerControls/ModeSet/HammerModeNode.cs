using System;
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
        [SerializeField] private string title;
        [SerializeField] private List<HammerMode> modeVariants = new();

        public override string Title => string.IsNullOrEmpty(title) ? name : title;
        
        public HammerMode ApplicableMode(Hammer data) => modeVariants.FirstOrDefault(data.IsUnlocked);

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
            instance.modeVariants.Add(mode);
            return instance;
        }
        public void DEBUG_ADDTRANS(HammerModeNodeTransition trans)
        {
            connections.Add(trans);
        }
        
        public override HammerModeNode Clone()
        {
            var clone = base.Clone();
            clone.modeVariants = new List<HammerMode>(modeVariants);
            return clone;
        }

        public override ConnectionCount InputConnection => ConnectionCount.Multi;
        public override ConnectionCount OutputConnection => ConnectionCount.Multi;
    }
}