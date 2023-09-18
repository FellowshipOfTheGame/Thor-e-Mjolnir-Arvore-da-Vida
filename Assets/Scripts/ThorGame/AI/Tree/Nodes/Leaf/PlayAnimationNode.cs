using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class PlayAnimationNode : LeafNode
    {
        [SerializeField] private string animationName;
        [SerializeField] private bool waitForEnd;

        private Animator _animator;
        private bool _justStarted;
        protected override void Init(AIData data)
        {
            _animator = data.Runner.GetComponent<Animator>();
            Debug.Log("start "+ animationName);
            _animator.Play(animationName);
        }

        protected override NodeResult Process(AIData data)
        {
            if (!waitForEnd) return NodeResult.Success;
            
            var state = _animator.GetCurrentAnimatorStateInfo(0);
            if (InitThisFrame || (state.IsName(animationName) && state.normalizedTime < 1.0))
            {
                return NodeResult.Running;
            }
            return NodeResult.Success;
        }
    }
}