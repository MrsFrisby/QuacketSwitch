using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private Button learnButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {//lambda expression
            //SceneManager.LoadScene(1);
            Loader.Load(Loader.Scene.QuacketSwitch);
        });

        learnButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Tutorial);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        //reset deltaTime when restarting after pause
        Time.timeScale = 1f;
    }

}
