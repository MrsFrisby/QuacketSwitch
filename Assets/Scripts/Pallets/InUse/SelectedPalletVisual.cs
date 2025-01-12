using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPalletVisual : MonoBehaviour
{

    [SerializeField] private BasePallet basePallet;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedPalletChanged += Player_OnSelectedPalletChanged;
    }

    private void Player_OnSelectedPalletChanged(object sender, Player.OnSelectedPalletChangedEventArgs e)
    {
        if (e.selectedPallet == basePallet)
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
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
        
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

}
