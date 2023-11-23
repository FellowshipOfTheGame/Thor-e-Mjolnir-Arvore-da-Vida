using System;
using System.Collections;
using UnityEngine;

namespace ThorGame
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSequence : MonoBehaviour
    {
        [Serializable]
        private struct ClipEntry
        {
            public AudioClip clip;
            public bool loop;
        }
        [SerializeField] private ClipEntry[] clips;

        private AudioSource _source;
        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.Stop();
            StartCoroutine(PlaySequenceCoroutine());
        }

        private IEnumerator PlaySequenceCoroutine()
        {
            foreach (var clip in clips)
            {
                _source.clip = clip.clip;
                _source.loop = clip.loop;
                _source.Play();
                if (clip.loop) yield break;
                yield return new WaitUntil(() => !_source.isPlaying);
            }
        }
    }
}