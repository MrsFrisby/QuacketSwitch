using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerPallet : BasePallet
{
    [SerializeField]
    private CookingSO[] CookingSOArray;



    public override void Interact(Player player)
    {
        if (!HasDuckObject())
        {//no duck already on pallet
            if (player.HasDuckObject())
                
            {//if player is carrying a duck
                Debug.Log("carrying a " + player.GetDuckObject().GetDucksSO());
                if (HasMatchwithSOCookingInput(player.GetDuckObject().GetDucksSO()))
                {//if duck dropped matches CookingSO.input duck object within pallet's array

                    //when E is pressed the duck is parented to this pallet
                    player.GetDuckObject().SetDuckObjectParent(this);
                }
                else
                {
                    Debug.Log("No matching. this is a "+ player.GetDuckObject().GetDucksSO());
                }
            }
            else
            {//duck already on pallet
                if (player.HasDuckObject())
                {//player already has a duck so can't pick up another

                }
                else
                { //if player empty handed give duck to player
                    GetDuckObject().SetDuckObjectParent(player);
                }
            }
        }
    }

    private bool HasMatchwithSOCookingInput(DucksSO inputDuckSO)
    {
        CookingSO cookingSO = GetCookingSOWithInput(inputDuckSO);
        if(cookingSO != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //this function takes in a ducksSO, matches it to the input of one of the items in the assemblySOarray
    // and returns the output, a ducksSO
    private DucksSO GetOutputForInput(DucksSO inputDuckSO)
    {
        CookingSO cookingSO = GetCookingSOWithInput(inputDuckSO);
        if (cookingSO != null)
        {
            return cookingSO.output;
        }
        else
        {
            return null;
        }
    }

    private CookingSO GetCookingSOWithInput(DucksSO inputDucksSO)

    {
        foreach (CookingSO cookingSO in CookingSOArray)
        {
            if (cookingSO.input == inputDucksSO)
            {
                return cookingSO;
            }
        }
        return null;
    }
   }
