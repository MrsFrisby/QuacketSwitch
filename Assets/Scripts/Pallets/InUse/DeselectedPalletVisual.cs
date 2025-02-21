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
        
        container = this.GetComponentInParent<Container>();
    }

    private void Start()
    {
        //foreach (GameObject gameObject in visualGameObjectArray)
        //{
        //    Debug.Log(container.name+": "+gameObject.name);
        //}
        //AssemblyPalletDuckHoles.OnDuckDelivered += AssemblyPalletDuckHoles_OnDuckDelivered;
        //Debug.Log("DPV:"+container.name);
        Hide();
        AssemblyPalletDuckHoles.onDuckAssembled += AssemblyPalletDuckHoles_onDuckAssembled;
        AssemblyPalletDuckHoles.OnDuckDelivered += AssemblyPalletDuckHoles_OnDuckDelivered;
    }

    private void AssemblyPalletDuckHoles_OnDuckDelivered(object sender, AssemblyPalletDuckHoles.OnDuckDeliveredEventArgs e)
    {
        //Debug.Log("DPV:APDH_OnFTPDuckDelivered: with" + e.containerDuckSO);
        if (e.containerDuckSO == thisContainerDucksSO)
        {
            //Debug.Log("Match with " + thisContainerDucksSO);
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
        //Debug.Log("DPV:Show()");
        //container.isDeactivated = true;
        for (int i = 0; i < visualGameObjectArray.Length; i++)
        {
            GameObject visualGameObject = visualGameObjectArray[i];
            if (visualGameObject != null)
            {
                //Container container = this.GetComponentInParent<Container>();
                
                visualGameObject.SetActive(true);
                //Debug.Log("DVP: Show: foreach " + visualGameObject.name);
            }
            else
            {
                Debug.Log("This game object is null: "+visualGameObject);
            }
        }
    }

    private void Hide()
    {
        
        // container.isDeactivated = false;
        //Debug.Log("DPV:Hide():" + container.name);
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            if (visualGameObject != null)
            {
                //Container container = this.GetComponentInParent<Container>();
                visualGameObject.SetActive(false);
                //Debug.Log("DVP: Show: foreach " + visualGameObject.name);
            }
            else
            {
                Debug.Log("This game object is null: " + visualGameObject);
            }
        }
    }
}
