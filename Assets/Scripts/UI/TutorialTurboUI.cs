using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TutorialTurboUI : MonoBehaviour
{
    //singleton pattern
    public static TutorialTurboUI Instance { get; private set; }

    [SerializeField]
    private string startPrompt;

    [SerializeField]
    private Sprite startSprite;

    //text
    [SerializeField]
    private string tPressedText;

    //image container for icon sprite
    [SerializeField]
    private Sprite tPressedSprite;

    [SerializeField]
    private TextMeshProUGUI tutorialBodyText;

    [SerializeField]
    private Image tutorialBodyIconImage;

    [SerializeField]
    private GameObject tutorialPanel;

    public event EventHandler OnShowTurbo;

    //private bool isTPressed;

    private float showTimer;
    //private float showTimerMax = 3f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Show();
        Hide();
        Timer.Instance.OnReset += Timer_OnReset;
        Player.Instance.OnBoost += Player_OnBoost;
        GameManager.Instance.OnStart += GameManager_OnStart;
        //isTPressed = false;
        tutorialBodyText.text = startPrompt;
        tutorialBodyIconImage.sprite = startSprite;
    }

    private void Timer_OnReset(object sender, System.EventArgs e)
    {
        Debug.Log("TTUI: Reset");
        Hide();
    }

    private void Update()
    {
        showTimer += Time.deltaTime;
        //Debug.Log(Time.deltaTime);
    }


    private void GameManager_OnStart(object sender, System.EventArgs e)
    {
        Debug.Log("TurboTutorial: OnStart event received");
        Show();
    }

    private void Player_OnBoost(object sender, System.EventArgs e)
    {
        //isTPressed = true;
        SetBoostText();
        Show();
    }

    //private void Update()
    //{
    //    showTimer += Time.deltaTime;
    //    if (showTimer > showTimerMax)
    //    {
    //        Hide();
    //    }
    //    LogTimer();
        
    //}


    private void Show()
    {
        OnShowTurbo?.Invoke(this, EventArgs.Empty);
        Debug.Log("TurboTutorial: Show()");
        //showTimer = 0f;
        tutorialPanel.SetActive(true);
        //LogTimer();
    }

    private void Hide()
    {
        tutorialPanel.SetActive(false);
        //LogTimer();
    }

    private void SetBoostText()
    {
        tutorialBodyText.text = tPressedText;
        tutorialBodyIconImage.sprite = tPressedSprite;
        
    }

    //private void LogTimer()
    //{
    //    Debug.Log("ShowTimer: " + showTimer);
    //}
}
