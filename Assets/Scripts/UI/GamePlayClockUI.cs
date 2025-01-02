using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayClockUI : MonoBehaviour
{
    [SerializeField]
    private Image timerImage;

    [SerializeField]
    private Gradient clockFillGradient;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayTimerNormalized();
        timerImage.color = GetprogressBarColour();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Show();
            
        }
        else
        {
            Hide();
        }
    }
    private Color GetprogressBarColour()
    {
        Color currentColour = clockFillGradient.Evaluate(timerImage.fillAmount);
        return currentColour;
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
