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

    public event EventHandler OnShowTurbo;

    [SerializeField]
    private string startPrompt;
    [SerializeField]
    private Sprite startSprite;
    
    [SerializeField]//text
    private string tPressedText;
    [SerializeField]//image container for icon sprite
    private Sprite tPressedSprite;

    [SerializeField]
    private TextMeshProUGUI tutorialBodyText;
    [SerializeField]
    private Image tutorialBodyIconImage;
    [SerializeField]

    private GameObject tutorialPanel;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Hide();
        Timer.Instance.OnReset += Timer_OnReset;
        Player.Instance.OnBoost += Player_OnBoost;
        GameManager.Instance.OnStart += GameManager_OnStart;
        tutorialBodyText.text = startPrompt;
        tutorialBodyIconImage.sprite = startSprite;
    }

    private void Timer_OnReset(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnStart(object sender, System.EventArgs e)
    {
        //Debug.Log("TurboTutorial: OnStart event received");
        Show();
    }

    private void Player_OnBoost(object sender, System.EventArgs e)
    {
        SetBoostText();
        Show();
    }

    private void Show()
    {
        OnShowTurbo?.Invoke(this, EventArgs.Empty);
        //Debug.Log("TurboTutorial: Show()");
        tutorialPanel.SetActive(true);
    }

    private void Hide()
    {
        tutorialPanel.SetActive(false);
    }

    private void SetBoostText()
    {
        tutorialBodyText.text = tPressedText;
        tutorialBodyIconImage.sprite = tPressedSprite;
        
    }
}
