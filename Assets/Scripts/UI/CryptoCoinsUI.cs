using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CryptoCoinsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinBalanceText;
    private float coinBalance;

    private void Start()
    {
        coinBalance = 0.0f;
        Player.Instance.OnImpact += Player_OnImpact;
        AssemblyPalletDuckHoles.OnDuckDelivered += AssemblyPalletDuckHoles_OnDuckDelivered;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        AssemblyManager.Instance.OnProtocolSuccess += AssemblyManager_OnProtocolSuccess;
        Hide();
    }

    private void AssemblyManager_OnProtocolSuccess(object sender, System.EventArgs e)
    {
        coinBalance = coinBalance + 5.78f;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Show();
        }
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGameOver(object sender, GameManager.OnGameOverEventArgs e)
    {
        Hide();
    }

    private void AssemblyPalletDuckHoles_OnDuckDelivered(object sender, AssemblyPalletDuckHoles.OnDuckDeliveredEventArgs e)
    {
        coinBalance = coinBalance + 1.34f;
    }

    private void Player_OnImpact(object sender, System.EventArgs e)
    {
        coinBalance = coinBalance - 0.63f;
    }

    private void Update()
    {
        //can't update balance on every frame!!!
        //int sucessfulQuackets = AssemblyManager.Instance.GetSuccessfulQuackets();
        //coinBalance = coinBalance+(float)(sucessfulQuackets * 5.78);
        coinBalanceText.text = coinBalance.ToString("0.00");
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public float GetCryptoCoinBalance()
    {
        coinBalance = (float)Math.Round(coinBalance, 2);
        return coinBalance;
    }
}
