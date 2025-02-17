using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectedPalletVisual : MonoBehaviour
{
    [SerializeField] private Container container;
    [SerializeField] private GameObject[] visualGameObjectArray;
    //[SerializeField] private AssemblyPalletDuckHoles assemblyPalletDuckHoles;

    private void Start()
    {
        //Player.Instance.OnSelectedPalletChanged += Player_OnSelectedPalletChanged;
        AssemblyPalletDuckHoles.Instance.OnDuckDelivered += AssemblyPalletDuckHoles_OnDuckDelivered;
        Debug.Log("DPV:"+container.name);
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
            Hide();
            Debug.Log("DPV:APDH-ODD: No Match");
        }
    }

    //private void Player_OnSelectedPalletChanged(object sender, Player.OnSelectedPalletChangedEventArgs e)
    //{
        //if (e.selectedPallet == basePallet)
        //{
            //Show();
        //}
        //else
        //{
            //Hide();
        //}
    //}

    private void Show()
    {
        Debug.Log("DPV:Show()");

        //container.DeactivatePallet();

        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            container.isDeactivated = true;
            visualGameObject.SetActive(true);
            Debug.Log("DVP: Show: foreach " + visualGameObject.name);
        }

    }

    private void Hide()
    {
        Debug.Log("DPV:Hide()");
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            container.isDeactivated = false;
            visualGameObject.SetActive(false);
            Debug.Log("DVP: Hide: foreach " + visualGameObject.name);
        }
    }
}
