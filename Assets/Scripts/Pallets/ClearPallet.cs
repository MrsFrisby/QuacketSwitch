using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPallet : BasePallet
{
    [SerializeField]
    private DucksSO ducksSO;

    //[SerializeField]
    //private Transform palletTopPoint;

    //private DuckObject duckObject;


    public override void Interact(Player player)
    {

        if (!HasDuckObject())
        {   //no duck already on pallet
            if (player.HasDuckObject())
            {
                //if player is carrying a duck - when E is pressed the duck is parented to this pallet
                player.GetDuckObject().SetDuckObjectParent(this);
            }
            else
            {
                //can't do anything, no ducks!
            }
        }
        else
        {
            //duck already on pallet
            if (player.HasDuckObject())
            {
                //player already has a duck so can't pick up another
            }
            else
            {
                //if player empty handed give duck to player
                GetDuckObject().SetDuckObjectParent(player);
            }
        }
    }

    
}
