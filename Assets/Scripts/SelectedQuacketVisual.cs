using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedQuacketVisual : MonoBehaviour
{

    [SerializeField] private Quacket quacket;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedQuacketChanged += Player_OnSelectedQuacketChanged;
    }

    private void Player_OnSelectedQuacketChanged(object sender, Player.OnSelectedQuacketChangedEventArgs e)
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
