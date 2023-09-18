using ThorGame.Player;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class ChargeNode : TargetedNode
    {
        [SerializeField] private float movePastDistance;
        
        private PlayerMover _mover;
        private float _startX;
        private float _targetX;
        private float _dirX;
        protected override void Init(AIData data)
        {
            base.Init(data);
            _mover = data.Runner.GetComponent<PlayerMover>();

            _startX = data.Runner.transform.position.x;
            _targetX = Target.position.x;
            _dirX = Mathf.Sign(_targetX - _startX);
            _targetX += _dirX * movePastDistance;
        }

        protected override NodeResult Process(AIData data)
        {
            _mover.SetHorizontalMovement(_dirX);

            float distanceCharged = Mathf.Abs(data.Runner.transform.position.x - _startX);
            float distanceToCharge = Mathf.Abs(_targetX - _startX);
            if (distanceCharged >= distanceToCharge)
            {
                _mover.SetHorizontalMovement(0);
                return NodeResult.Success;
            }
            return NodeResult.Running;

            /*float curOffFromStart = data.Runner.transform.position.x - _startX;
            float targetOffFromStart = _targetX - _targetX;
            
            
            
            if ((int)Mathf.Sign(targetOffFromStart) == (int)Mathf.Sign(curOffFromStart)
                && Mathf.Abs(curOffFromStart) >= Mathf.Abs(targetOffFromStart))
            {
                return NodeResult.Success;
            }
            return NodeResult.Running;*/
        }
    }
}