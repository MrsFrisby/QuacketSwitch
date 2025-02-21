using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI quacketsDeliveredText;

    [SerializeField]
    private TextMeshProUGUI cryptoEarnedText;

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
            quacketsDeliveredText.text = AssemblyManager.Instance.GetSuccessfulQuackets().ToString();
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
