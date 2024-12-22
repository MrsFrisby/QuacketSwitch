using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyPallet : BasePallet

{
    [SerializeField]
    private DucksSO assembledQuacket;


    public override void Interact(Player player)
    {
        Debug.Log("E pressed");

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



    public override void InteractAlternate(Player player)
    {
        Debug.Log("F pressed");
       
        //duck already on pallet
        if (HasDuckObject())
        {
            GetDuckObject().DestroySelf();
            Transform duckTransform = Instantiate(assembledQuacket.prefab);
            duckTransform.GetComponent<DuckObject>().SetDuckObjectParent(this);
        }
        else
        {
            
        }
        
    }
}
