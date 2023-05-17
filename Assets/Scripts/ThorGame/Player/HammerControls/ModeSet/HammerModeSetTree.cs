using System.Collections.Generic;
using ThorGame.Player.HammerControls.Modes;
using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    [CreateAssetMenu(fileName = "HammerModeSetTree", menuName = "HammerModes/Set", order = 0)]
    public class HammerModeSetTree : ScriptableObject, ICloneable<HammerModeSetTree>, ITypedTree<HammerModeNode, Hammer, HammerModeNode>
    {
        [HideInInspector] [SerializeField] private HammerModeNode rootNode;
        [SerializeField] private List<HammerModeNode> allNodes = new();
        
        public HammerModeNode Root => rootNode;
        public IEnumerable<HammerModeNode> AllNodes => allNodes;

        [SerializeField]
        private HammerModeNode _currentNode;
        public void Tick(Hammer data)
        {
            if (!_currentNode)
            {
                StartNode(data, rootNode);
            }
            
            var newNode = _currentNode.Tick(data);
            if (newNode != null) StartNode(data, newNode);
        }

        private void StartNode(Hammer data, HammerModeNode node)
        {
            if (_currentNode) _currentNode.End(data);
            _currentNode = node;
            _currentNode.Begin(data);
        }

        public HammerModeSetTree Clone()
        {
            var clone = Instantiate(this);
            clone.allNodes = allNodes.ConvertAll(n => n.Clone());
            clone.rootNode = rootNode ? clone.allNodes[allNodes.IndexOf(rootNode)] : null;
            return clone;
        }
        
        //TODO DEBUG
        public static HammerModeSetTree DEBUG_INSTANCE(SlamHammerMode slam, PrepareThrowHammerMode prepare, ThrownHammerMode thrown)
        {
            var instance = CreateInstance<HammerModeSetTree>();

            var heldNode = HammerModeNode.DEBUG_GETINSTANCE(slam);
            var prepNode = HammerModeNode.DEBUG_GETINSTANCE(prepare);
            var throNode = HammerModeNode.DEBUG_GETINSTANCE(thrown);
            
            heldNode.DEBUG_ADDTRANS(HammerModeKeyTransition.DEBUG_INSTANCE(heldNode, prepNode, KeyCode.R));
            heldNode.DEBUG_ADDTRANS(HammerModeKeyTransition.DEBUG_INSTANCE(heldNode, throNode, KeyCode.T));
            
            prepNode.DEBUG_ADDTRANS(HammerModeKeyTransition.DEBUG_INSTANCE(prepNode, heldNode, KeyCode.R));
            prepNode.DEBUG_ADDTRANS(HammerModeKeyTransition.DEBUG_INSTANCE(prepNode, throNode, KeyCode.T));
            
            instance.allNodes.AddRange(new []
            {
                heldNode, prepNode, throNode
            });
            instance.rootNode = heldNode;

            return instance;
        }
    }
}