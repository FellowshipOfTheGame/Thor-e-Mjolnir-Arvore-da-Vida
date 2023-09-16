using System.Collections.Generic;
using UnityEngine;

namespace ThorGame
{
    public class DestroyOnBlocksDestroyed : MonoBehaviour
    {
        private HashSet<HittableBlock> _blocks = new();
        private void Awake()
        {
            foreach (var block in GetComponentsInChildren<HittableBlock>())
            {
                _blocks.Add(block);
                block.breakEvent.AddListener(() => OnBlockBreak(block));
            }
        }

        private void OnBlockBreak(HittableBlock block)
        {
            _blocks.Remove(block);
            if (_blocks.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}