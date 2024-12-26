using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerPalletVisual : MonoBehaviour
{
    [SerializeField]
    private CookerPallet cookerPallet;

    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private GameObject particles;


    private void Start()
    {
        cookerPallet.OnStateChanged += CookerPallet_OnStateChanged;
    }

    private void CookerPallet_OnStateChanged(object sender, CookerPallet.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == CookerPallet.State.Green || e.state == CookerPallet.State.Red;
        Debug.Log(showVisual);
        smoke.SetActive(showVisual);
        particles.SetActive(showVisual);
    }
}
