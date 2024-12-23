using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]
    private Image progressBarImage;

    [SerializeField]
    private AssemblyPallet assemblyPallet;

    private void Start()
    {
        assemblyPallet.OnProgressChanged += AssemblyPallet_OnProgressChanged;
        progressBarImage.fillAmount = 0f;

        Hide();
    }

    private void AssemblyPallet_OnProgressChanged(object sender, AssemblyPallet.OnProgressChangedEventArgs e)
    {
        progressBarImage.fillAmount = e.progressNormalized;

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
}
