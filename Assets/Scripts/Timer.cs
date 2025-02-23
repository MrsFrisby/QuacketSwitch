using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //singleton pattern
    public static Timer Instance { get; private set; }

    private float timer;

    [SerializeField]
    private float timerMax = 2.5f;

    public event EventHandler OnReset;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameManager.Instance.OnStart += GameManager_OnStart;
        TutorialTurboUI.Instance.OnShowTurbo += TutorialTurboUI_OnShowTurbo;
    }

    private void TutorialTurboUI_OnShowTurbo(object sender, EventArgs e)
    {
        timer = 0f;
    }

    private void GameManager_OnStart(object sender, System.EventArgs e)
    {
        timer = 0f;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            TimerReset();
        }
        //Debug.Log("Timer: " + timer);
    }

    private void TimerReset()
    {
        timer = 0f;
        OnReset?.Invoke(this, EventArgs.Empty);
    }
}
