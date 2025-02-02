using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionWarningUI : MonoBehaviour
{
    [SerializeField]
    private AssemblyPalletDuckHoles assemblyPalletDuckHoles;

    private void Start()
    {
        assemblyPalletDuckHoles.OnProgressChanged += AssemblyPalletDuckHoles_OnProgressChanged;
        Hide();
    }

    private void AssemblyPalletDuckHoles_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float corruptShowProgressAmount = .5f;

        bool show = assemblyPalletDuckHoles.IsAssembled() && e.progressNormalized >= corruptShowProgressAmount;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
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
