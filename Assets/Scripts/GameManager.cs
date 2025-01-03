using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private enum GameState
    {
        WaitingToStart,
        Starting,
        GamePlaying,
        GameOver,
    }

    private GameState gameState;
    private float waitingToStartTimer = 1f;
    private float startCountdownTimer = 5f;
    private float gamePlayTimer;
    private float gamePlayTimerMax = 15f;
    private bool IsGamePaused = false;


    private void Awake()
    {
        Instance = this;
        gameState = GameState.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    gameState = GameState.Starting;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case GameState.Starting:
                startCountdownTimer -= Time.deltaTime;
                if (startCountdownTimer < 0f)
                {
                    gameState = GameState.GamePlaying;
                    gamePlayTimer = gamePlayTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case GameState.GamePlaying:
                gamePlayTimer -= Time.deltaTime;
                if (gamePlayTimer < 0f)
                {
                    gameState = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case GameState.GameOver:
                break;
        }
        //Debug.Log(gameState);
    }

    public bool IsGamePlaying()
    {
        return gameState == GameState.GamePlaying;
    }

    public bool IsCountdownActive()
    {
        return gameState == GameState.Starting;
    }

    public bool IsGameOver()
    {
        return gameState == GameState.GameOver;
    }

    public float GetCountdownTimer()
    {
        return startCountdownTimer;
    }

    public float GetGamePlayTimerNormalized()
    {
        return 1 -(gamePlayTimer / gamePlayTimerMax);
    }


    public void TogglePauseGame ()
    {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
