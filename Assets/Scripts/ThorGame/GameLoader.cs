using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThorGame
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private string gameScene, menuScene;
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

        public void LoadGame() => LoadGame(false);
        public void LoadGame(bool delayed)
        {
            if (delayed) StartCoroutine(DelayedLoadCoroutine(gameScene));
            else SceneManager.LoadScene(gameScene);
        }

        public void LoadMenu() => LoadMenu(false);
        public void LoadMenu(bool delayed)
        {
            if (delayed) StartCoroutine(DelayedLoadCoroutine(menuScene));
            else SceneManager.LoadScene(menuScene);
        }
    }
}