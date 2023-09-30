using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThorGame
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private string gameScene, menuScene, victoryScene, defeatScene;
        [SerializeField] private float delaySeconds;
    
        public static GameLoader Instance { get; private set; }
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
        private IEnumerator DelayedLoadCoroutine(string scene)
        {
            yield return new WaitForSeconds(delaySeconds);
            SceneManager.LoadScene(scene);
        }

        private void Load(string scene, bool delayed)
        {
            if (delayed) StartCoroutine(DelayedLoadCoroutine(scene));
            else SceneManager.LoadScene(scene);
        }

        public void LoadGame(bool delayed) => Load(gameScene, delayed);
        public void LoadMenu(bool delayed) => Load(menuScene, delayed);
        public void LoadVictory(bool delayed) => Load(victoryScene, delayed);
        public void LoadDefeat(bool delayed) => Load(defeatScene, delayed);

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}