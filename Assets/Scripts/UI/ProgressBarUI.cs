using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]
    private GameObject hasProgressGameObject;

    [SerializeField]
    private Image progressBarImage;

    [SerializeField]
    private Gradient progressBarGradient;

    private IHasProgress hasProgress;
    //private Color currentColour;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        progressBarImage.fillAmount = 0f;
        progressBarImage.color = GetprogressBarColour();

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        progressBarImage.fillAmount = e.progressNormalized;
        //currentColour = progressBarGradient.Evaluate(e.progressNormalized);
        progressBarImage.color = GetprogressBarColour();

        if (e.progressNormalized ==0f || e.progressNormalized ==1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private Color GetprogressBarColour()
    {
        Color currentColour = progressBarGradient.Evaluate(progressBarImage.fillAmount);
        return currentColour;
    }
}
