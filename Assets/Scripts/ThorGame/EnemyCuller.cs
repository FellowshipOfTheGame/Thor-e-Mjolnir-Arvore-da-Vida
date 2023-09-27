﻿using System.Collections.Generic;
using ThorGame.Player;
using UnityEngine;

namespace ThorGame
{
    public class EnemyCuller : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float padding;
        private readonly List<Collider2D> _enemies = new();
        private readonly Dictionary<Collider2D, Bounds> _boundsCache = new();
        private void Start()
        {
            foreach (var enemy in FindObjectsOfType<EntityHealth>())
            {
                if (enemy.GetComponent<ThorController>()) continue;
                var col = enemy.GetComponent<Collider2D>();
                _enemies.Add(col);
                _boundsCache.Add(col, col.bounds);
            }
            CullAll();
        }

        private Bounds CamBounds()
        {
            float ortSize = cam.orthographicSize;
            Vector3 camPos = cam.transform.position;
            camPos.z = 0;
            var bounds = new Bounds
            {
                center = camPos,
                extents = new Vector3(ortSize * Screen.width / Screen.height + padding, ortSize + padding, 1)
            };
            Vector3 pad = Vector3.one * padding;
            bounds.min -= pad;
            bounds.max += pad;
            return bounds;
        }

        private Bounds ColBounds(Collider2D col)
        {
            if (col.gameObject.activeSelf)
            {
                _boundsCache[col] = col.bounds;
            }
            return _boundsCache[col];
        }

        private void CullAll()
        {
            var camBounds = CamBounds();
            foreach (var enemy in _enemies)
            {
                enemy.gameObject.SetActive(ColBounds(enemy).Intersects(camBounds));
            }
        }

        private void LateUpdate()
        {
            CullAll();
        }
    }
}