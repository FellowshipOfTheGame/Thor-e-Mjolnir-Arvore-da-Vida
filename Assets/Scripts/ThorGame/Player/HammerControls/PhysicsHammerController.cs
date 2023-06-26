using System;
using UnityEngine;

namespace ThorGame.Player.HammerControls
{
    public class PhysicsHammerController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Rigidbody2D hammerParent;
        [SerializeField] private Transform hammer;

        [Header("Range")] 
        [SerializeField] private Transform clampOrigin;
        [SerializeField] private Vector2 originOffset;
        [SerializeField] private float maxRange;

        private Quaternion _ogRotation;
        private void Start()
        {
            _ogRotation = hammer.rotation; //clampOrigin.worldToLocalMatrix.inverse.rotation * hammer.rotation;
        }

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
            target.position = CalcTargetPos();
        }

        private void FixedUpdate()
        {
            hammerParent.MovePosition(clampOrigin.position + clampOrigin.TransformDirection(originOffset));
            hammerParent.SetRotation(clampOrigin.rotation * _ogRotation);
        }
    }
}