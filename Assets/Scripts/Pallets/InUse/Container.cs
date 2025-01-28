using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : BasePallet
{
    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField]
    private DucksSO ducksSO;

    public override void Interact(Player player)
    {
        //if the player is not already carrying something
        if (!HasDuckObject())
        {
            DuckObject.spawnDuckObject(ducksSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("The player already has a duck");
        }
    }
}
