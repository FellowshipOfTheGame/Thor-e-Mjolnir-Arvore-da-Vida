using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThorGame.Player
{
    [Serializable]
    public class GroundChecker
    {
        [SerializeField] private float castDistance = 0.1f;
        [SerializeField] private LayerMask mask;
    
        public float LastGroundedChangeTime { get; private set; }
        public bool IsGrounded => GroundHit;
        public RaycastHit2D GroundHit { get; private set; }

        public void UpdateGrounded(Collider2D collider)
        {
            ContactFilter2D filter = new ContactFilter2D { layerMask = mask };
            List<RaycastHit2D> hits = new();
            int hitCount = collider.Cast(Vector2.down, filter, hits, distance: castDistance);
            var hit = hitCount > 0 ? hits[0] : default;
            if (hit != GroundHit) LastGroundedChangeTime = Time.time;
            GroundHit = hit;
        }
    }
}