using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button goBt, getOutBt;
    void Start()
    {

        goBt.onClick.AddListener(LoadScene);
        getOutBt.onClick.AddListener(GetOut);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadScene()
    {

        SceneManager.LoadScene("qqcena");

    }

    void GetOut()
    {

        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }
}
