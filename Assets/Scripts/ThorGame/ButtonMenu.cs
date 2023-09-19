using UnityEngine;
using UnityEngine.UI;

namespace ThorGame
{
    public class ButtonMenu : MonoBehaviour
    {
        [SerializeField] private Button goBt, getOutBt;
        private void Start()
        {
            goBt.onClick.AddListener(GameLoader.Instance.LoadGame);
            getOutBt.onClick.AddListener(GetOut);
        }

        private static void GetOut()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}