using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : BasePallet
{
    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField]
    private DucksSO ducksSO;

    [SerializeField]
    private GameObject deselectedVisual;

    public bool isDeactivated = false;

    public void Start()
    {
        //ReactivatePallet();
    }

    public override void Interact(Player player)
    {
        //if the player is not already carrying something
        if (!HasDuckObject() && !isDeactivated)
        {
            DuckObject.spawnDuckObject(ducksSO, player);
            //DeactivatePallet();
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("The player already has a duck or the pallet is deactivated");
        }
    }

    public void DeactivatePallet()
    {
        deselectedVisual.SetActive(true);
        isDeactivated = true;
    }

    public void ReactivatePallet()
    {
        deselectedVisual.SetActive(false);
        isDeactivated = false;
    }
}
