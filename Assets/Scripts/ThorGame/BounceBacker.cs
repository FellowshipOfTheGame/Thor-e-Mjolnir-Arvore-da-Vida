using UnityEngine;

namespace ThorGame
{
    public class BounceBacker : MonoBehaviour
    {
        [SerializeField] private float groundPadding;
        [SerializeField] private float bounceBackPadding;
        [SerializeField] private float startY;
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask groundMask;
        
        private void FixedUpdate()
        {
            Vector3 origin = transform.position;
            origin.y = startY;
            
            Debug.DrawRay(origin, Vector3.down * maxDistance, Color.yellow);
            var hit = Physics2D.Raycast(origin, Vector2.down, maxDistance, groundMask);
            if (!hit)
            {
                Debug.Log("BounceBacker missed ground!", gameObject);
                return;
            }

            var hitY = hit.point.y;
            if (transform.position.y < hitY - groundPadding)
            {
                Debug.Log("Bouce back", gameObject);
                transform.position = hit.point + Vector2.up * bounceBackPadding;
            }
        }
    }
}