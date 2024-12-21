using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPalletVisual : MonoBehaviour
{

    [SerializeField] private BasePallet pallet;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedPalletChanged += Player_OnSelectedPalletChanged;
    }

    private void Player_OnSelectedPalletChanged(object sender, Player.OnSelectedPalletChangedEventArgs e)
    {
        if (e.selectedPallet == pallet)
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
