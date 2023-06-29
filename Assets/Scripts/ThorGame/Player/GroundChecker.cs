using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class GroundChecker
{
    [SerializeField] private float castDistance = 0.1f;
    [SerializeField] private LayerMask mask;

    
    private RaycastHit2D[] _groundHits = new RaycastHit2D[3];
    public bool IsGrounded => _groundHits.Any(h => h.collider);
    public RaycastHit2D DirectionalHit(bool right) =>
        right 
            ? _groundHits.LastOrDefault(h => h) 
            : _groundHits.FirstOrDefault(h => h);

    public RaycastHit2D DirectionalHit(float movement) => movement switch
    {
        > 0 => _groundHits.LastOrDefault(h => h),
        < 0 => _groundHits.FirstOrDefault(h => h),
        _   => _groundHits.Where(h=>h).ElementAtOrDefault(1)
    };
    
    

    private static Vector2[] CalcOrigins(Bounds bounds)
    {
        float halfHeight = bounds.size.y / 2;
        float halfWidth = bounds.size.x / 2;
        float bottomY = bounds.center.y - halfHeight;
        float centerX = bounds.center.x;
        return new[]
        {
            new Vector2(centerX, bottomY),
            new Vector2(centerX - halfWidth, bottomY),
            new Vector2(centerX + halfWidth, bottomY)
        };
    }
    
    public void UpdateGrounded(Bounds bounds)
    {
        Vector2[] origins = CalcOrigins(bounds);
        for (int i = 0; i < origins.Length; i++)
        {
            _groundHits[i] = Physics2D.Raycast(origins[i], Vector2.down, castDistance, mask);
        }
    }

    public void DEBUG_Draw(Bounds bounds)
    {
        var origins = CalcOrigins(bounds);
        for (var i = 0; i < origins.Length; i++)
        {
            Debug.DrawRay(origins[i], Vector3.down * castDistance, _groundHits[i].collider ? Color.green : Color.red);
        }
    }
}