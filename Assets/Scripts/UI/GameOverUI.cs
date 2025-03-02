using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI transmissionsDeliveredText;

    [SerializeField]
    private TextMeshProUGUI cryptoEarnedText;

    [SerializeField]
    private TextMeshProUGUI quacketsCollectedText;

    [SerializeField]
    private CryptoCoinsUI cryptoCoinsUI;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
        //GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    //private void GameManager_OnGameOver(object sender, GameManager.OnGameOverEventArgs e)
    //{
    //    throw new System.NotImplementedException();
    //}

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            int transmissionsDelivered = AssemblyManager.Instance.GetSuccessfulTransmissions();
            int quacketsCollected = GameManager.Instance.GetQuacketsCollected();
            //quacketsDelivered = (float)Math.Round(quacketsDelivered, 2);
            transmissionsDeliveredText.text = transmissionsDelivered.ToString();
            quacketsCollectedText.text = quacketsCollected.ToString();
            //quacketsDeliveredText.text = AssemblyManager.Instance.GetSuccessfulQuackets().ToString("0.00");
            cryptoEarnedText.text = cryptoCoinsUI.GetCryptoCoinBalance().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        //constrain timer to integers
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
