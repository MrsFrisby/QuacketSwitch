using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDuckVisual : MonoBehaviour
{

    [SerializeField] private Quacket quacket;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        CrashTestPlayerController.Instance.OnSelectedQuacketChanged += Player_OnSelectedQuacketChanged;
    }

    private void Player_OnSelectedQuacketChanged(object sender, CrashTestPlayerController.OnSelectedQuacketChangedEventArgs e)
    {
        if (e.selectedQuacket == quacket)
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
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }

}
