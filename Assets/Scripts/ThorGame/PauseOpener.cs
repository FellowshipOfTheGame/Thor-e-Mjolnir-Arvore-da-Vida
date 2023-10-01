using UnityEngine;
using UnityEngine.Events;

namespace ThorGame
{
    public class PauseOpener : MonoBehaviour
    {
        [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
        [SerializeField] private Canvas pauseCanvas;

        [SerializeField] private UnityEvent pauseEvent;
        [SerializeField] private UnityEvent unpauseEvent;

        public bool Paused
        {
            get => pauseCanvas.gameObject.activeSelf;
            set
            {
                Time.timeScale = value ? 0 : 1;
                pauseCanvas.gameObject.SetActive(value);
                
                if (value) pauseEvent?.Invoke();
                else unpauseEvent?.Invoke();
            }
        }

        private void Awake() => Paused = false;
        private void Update()
        {
            if (Input.GetKeyDown(pauseKey)) Toggle();
        }

        public void Toggle() => Paused = !Paused;
        public void Pause() => Paused = true;
        public void UnPause() => Paused = false;
    }
}
