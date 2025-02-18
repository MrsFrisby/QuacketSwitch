using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectedPalletVisual : MonoBehaviour
{
    [SerializeField] private Container container;
    [SerializeField] private GameObject[] visualGameObjectArray;
    

    private void Start()
    {
        AssemblyPalletDuckHoles.OnDuckDelivered += AssemblyPalletDuckHoles_OnDuckDelivered;
        //Debug.Log("DPV:"+container.name);
        Hide();
        AssemblyPalletDuckHoles.onFTPDuckAssembled += AssemblyPalletDuckHoles_onFTPDuckAssembled;
    }

    private void AssemblyPalletDuckHoles_onFTPDuckAssembled(object sender, EventArgs e)
    {
        Hide();
    }

    private void AssemblyPalletDuckHoles_OnDuckDelivered(object sender, AssemblyPalletDuckHoles.OnDuckDeliveredEventArgs e)
    {
        Debug.Log("DPV:APDH_OnDuckDelivered"+e.containerToDeactivate);

        if (e.containerToDeactivate.name == container.name)
        {
            Show();
            Debug.Log("DPV:APDH-ODD: Match");
        }
        else
        {
            Debug.Log("DPV:APDH-ODD: No match between");
        }
        
    }

    private void Show()
    {
        Debug.Log("DPV:Show()");
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            container.isDeactivated = true;
            visualGameObject.SetActive(true);
            //Debug.Log("DVP: Show: foreach " + visualGameObject.name);
        }
    }

    private void Hide()
    {
        Debug.Log("DPV:Hide()");
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            container.isDeactivated = false;
            visualGameObject.SetActive(false);
            //Debug.Log("DVP: Hide: foreach " + visualGameObject.name);
        }
    }
}
