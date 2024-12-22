using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : BasePallet
{
    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField]
    private DucksSO ducksSO;

    //[SerializeField]
    //private Transform palletTopPoint;

    //private DuckObject duckObject;


    public override void Interact(Player player)
    {
        //if the player is not already carrying something
        if (!HasDuckObject())
        {
            Transform duckTransform = Instantiate(ducksSO.prefab);
            duckTransform.GetComponent<DuckObject>().SetDuckObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
           // duckObject.SetDuckObjectParent(player);
        }
    }

   
}
