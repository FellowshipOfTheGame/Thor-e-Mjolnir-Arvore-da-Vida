using System;
using UnityEngine;

namespace ThorGame
{
    [Serializable]
    public class Timer
    {
        [field: SerializeField]
        public float MaxSeconds { get; private set; }
        public float CurrentSeconds { get; private set; }
        public float Progress => MaxSeconds > 0 ? CurrentSeconds / MaxSeconds : 1;
        public bool Completed => CurrentSeconds >= MaxSeconds;

        public Timer()
        {
            MaxSeconds = 0;
        }
        
        public Timer(float seconds)
        {
            MaxSeconds = seconds;
        }

        public bool Tick()
        {
            CurrentSeconds += UnityEngine.Time.deltaTime;
            return Completed;
        }

        public void Reset() => CurrentSeconds = 0;
    }
}