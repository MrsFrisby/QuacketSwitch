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
                //player is carrying a duck
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
                //player already has a duck
            }
            else
            {
                //player empty handed
                GetDuckObject().SetDuckObjectParent(player);
            }
        }
    }

    
}
