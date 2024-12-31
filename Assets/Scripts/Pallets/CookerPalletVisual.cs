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
    private GameObject electricity;

    [SerializeField]
    private GameObject sparks;


    private void Start()
    {
        cookerPallet.OnStateChanged += CookerPallet_OnStateChanged;
    }

    private void CookerPallet_OnStateChanged(object sender, CookerPallet.OnStateChangedEventArgs e)
    {
        //bool showSmokeVisual = e.state == CookerPallet.State.Assembling || e.state == CookerPallet.State.Corrupting;
        bool showSmokeVisual = e.state == CookerPallet.State.Assembling;
        bool showSparksVisual = e.state == CookerPallet.State.Corrupting;
        bool showElectricVisual = e.state == CookerPallet.State.Corrupt;
        //Debug.Log(showVisual);

        if(showSmokeVisual)
        {
            smoke.SetActive(true);
            sparks.SetActive(false);
            electricity.SetActive(false);
        }
        else if(showSparksVisual)
        {
            smoke.SetActive(true);
            sparks.SetActive(true);
            electricity.SetActive(false);
        }
        else if(showElectricVisual)
        {
            smoke.SetActive(false);
            sparks.SetActive(true);
            electricity.SetActive(true);
        }
        
    }
}
