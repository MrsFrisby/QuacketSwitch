using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHolesUI : MonoBehaviour
{
    [SerializeField] private AssemblyPalletDuckHoles assemblyPalletDuckHoles;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        assemblyPalletDuckHoles.OnDuckSpawned += AssemblyPalletDuckHoles_OnDuckSpawned;

    }


    private void AssemblyPalletDuckHoles_OnDuckSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }



    private void UpdateVisual()
    {
        //delete all existing icons
        foreach (Transform child in transform)
        {
            //don't delete the icon template, just the icons/backgrounds
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        //recreate UI icon display from updated list
        foreach (DucksSO ducksSO in assemblyPalletDuckHoles.GetDucksSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<DuckHolesSingleUI>().setDuckObjectSO(ducksSO);
        }
    }
}

