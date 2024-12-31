using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerPalletHolesVisual : MonoBehaviour
{
    [SerializeField]
    private CookerPalletHoles cookerPalletHoles;

    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private GameObject electricity;

    [SerializeField]
    private GameObject sparks;


    private void Start()
    {
        cookerPalletHoles.OnStateChanged += CookerPalletHoles_OnStateChanged;
        
    }

    private void CookerPalletHoles_OnStateChanged(object sender, CookerPalletHoles.OnStateChangedEventArgs e)
    {
        
        bool showSmokeVisual = e.state == CookerPalletHoles.State.Assembling;
        bool showSparksVisual = e.state == CookerPalletHoles.State.Corrupting;
        bool showElectricVisual = e.state == CookerPalletHoles.State.Corrupt;
        

        if (showSmokeVisual)
        {
            smoke.SetActive(true);
            sparks.SetActive(false);
            electricity.SetActive(false);
        }
        else if (showSparksVisual)
        {
            smoke.SetActive(true);
            sparks.SetActive(true);
            electricity.SetActive(false);
        }
        else if (showElectricVisual)
        {
            smoke.SetActive(false);
            sparks.SetActive(true);
            electricity.SetActive(true);
        }
    }

    
}
