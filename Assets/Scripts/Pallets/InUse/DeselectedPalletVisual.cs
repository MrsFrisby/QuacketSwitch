using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectedPalletVisual : MonoBehaviour
{
    private Container container;
    [SerializeField] private GameObject[] visualGameObjectArray;
    [SerializeField] private DucksSO thisContainerDucksSO;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        container = this.GetComponentInParent<Container>();
    }

    private void Start()
    {
        //AssemblyPalletDuckHoles.OnDuckDelivered += AssemblyPalletDuckHoles_OnDuckDelivered;
        //Debug.Log("DPV:"+container.name);
        Hide();
        AssemblyPalletDuckHoles.onDuckAssembled += AssemblyPalletDuckHoles_onDuckAssembled;
        AssemblyPalletDuckHoles.OnDuckDelivered += AssemblyPalletDuckHoles_OnDuckDelivered;
    }

    private void AssemblyPalletDuckHoles_OnDuckDelivered(object sender, AssemblyPalletDuckHoles.OnDuckDeliveredEventArgs e)
    {
        Debug.Log("DPV:APDH_OnFTPDuckDelivered: with" + e.containerDuckSO);
        if (e.containerDuckSO == thisContainerDucksSO)
        {
            Debug.Log("Match with " + thisContainerDucksSO);
            Show();
        }
    }

    private void AssemblyPalletDuckHoles_onDuckAssembled(object sender, EventArgs e)
    {
        Hide();
    }

    //private void AssemblyPalletDuckHoles_OnDuckDelivered(object sender, AssemblyPalletDuckHoles.OnDuckDeliveredEventArgs e)
    //{
    //    //Debug.Log("DPV:APDH_OnDuckDelivered");
    //    //Debug.Log("DPV:APDH_OnDuckDelivered: Comparing:"+e.containerToDeactivate.name);
    //    Debug.Log("DPV:APDH_OnDuckDelivered: with" + container.name);

    //    //if (e.containerToDeactivate.name == container.name)
    //    //{
    //    //    Show();
    //    //    Debug.Log("DPV:APDH-ODD: Match");
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("DPV:APDH-ODD: No match between");
    //    //}

    //}

    private void Show()
    {
        Debug.Log("DPV:Show()");
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            //Container container = this.GetComponentInParent<Container>();
            container.isDeactivated = true;
            visualGameObject.SetActive(true);
            //Debug.Log("DVP: Show: foreach " + visualGameObject.name);
        }
    }

    private void Hide()
    {
        Debug.Log("DPV:Hide():"+container.name);
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            container.isDeactivated = false;
            visualGameObject.SetActive(false);
            //Debug.Log("DVP: Hide: foreach " + visualGameObject.name);
        }
    }
}
