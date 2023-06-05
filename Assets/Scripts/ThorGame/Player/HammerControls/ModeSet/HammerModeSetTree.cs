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
        
        public void OnCollide(Hammer hammer, IHittable hittable) => _currentNode.ApplicableMode(hammer).OnCollide(hammer, hittable);
    }
}