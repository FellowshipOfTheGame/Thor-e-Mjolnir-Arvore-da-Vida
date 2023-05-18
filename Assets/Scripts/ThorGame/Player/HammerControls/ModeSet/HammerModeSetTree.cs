using ThorGame.Player.HammerControls.Modes;
using ThorGame.Player.HammerControls.ModeSet.Transitions;
using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.Player.HammerControls.ModeSet
{
    [CreateAssetMenu(fileName = "HammerModeSetTree", menuName = "HammerModes/Set", order = 0)]
    public class HammerModeSetTree : TypedTree<HammerModeSetTree, HammerModeNode, HammerModeNodeTransition>
    {
        [SerializeField]
        private HammerModeNode _currentNode;
        public void Tick(Hammer data)
        {
            if (!_currentNode)
            {
                StartNode(data, root);
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
            instance.root = heldNode;

            return instance;
        }
    }
}