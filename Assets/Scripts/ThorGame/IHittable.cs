using UnityEngine;

namespace ThorGame
{
    public interface IHittable
    {
        void Hit(Vector2 point, Vector2 velocity);
    }
}