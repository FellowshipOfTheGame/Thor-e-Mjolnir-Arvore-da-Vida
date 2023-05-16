using UnityEngine;

namespace ThorGame.Player
{
    public class PhysicsHammerController : MonoBehaviour
    {
        [SerializeField] private TargetJoint2D target;
        [SerializeField] private Joint2D hinge;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private KeyCode throwKey;

        [Header("Range")] 
        [SerializeField] private Transform clampOrigin;
        [SerializeField] private float maxRange;

        private Vector2 CalcTargetPos()
        {
            Vector2 mousePixelPos = Input.mousePosition;
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePixelPos);

            Vector2 origin = clampOrigin.position;
            Vector2 offset = worldMousePos - origin;
            if (maxRange > 0)
            {
                offset =  Vector2.ClampMagnitude(offset, maxRange);
            }
            Debug.DrawRay(origin, offset, Color.magenta);
            return origin + offset;
        }
        
        private void Update()
        {
            if (!hinge.enabled) return;

            target.target = CalcTargetPos();
            if (Input.GetKeyDown(throwKey))
            {
                hinge.enabled = false;
                target.enabled = false;
                rb.transform.parent = null;
            }
        }
    }
}