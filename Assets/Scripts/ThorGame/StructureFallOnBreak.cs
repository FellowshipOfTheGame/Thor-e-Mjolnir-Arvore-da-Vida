using System.Linq;
using ThorGame;
using UnityEngine;

public class StructureFallOnBreak : MonoBehaviour
{
    private HittableBlock[] _blocks;
    private Rigidbody2D[] _rbs;

    private void EnablePhysics()
    {
        foreach (var rb in _rbs.Where(rb => rb))
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    
    private void DisablePhysics()
    {
        foreach (var rb in _rbs.Where(rb => rb))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void Awake()
    {
        _blocks = GetComponentsInChildren<HittableBlock>();
        _rbs = GetComponentsInChildren<Rigidbody2D>();
        DisablePhysics();
    }

    private void OnEnable()
    {
        foreach (var block in _blocks.Where(b => b))
        {
            block.breakEvent.AddListener(EnablePhysics);
        }
    }

    private void OnDisable()
    {
        foreach (var block in _blocks.Where(b => b))
        {
            block.breakEvent.RemoveListener(EnablePhysics);
        }
    }
}