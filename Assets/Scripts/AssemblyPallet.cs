using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyPallet : BasePallet

{
    [SerializeField]
    private AssemblySO[] assembledDucksSOArray;

    //[SerializeField]
    //private DucksSO assembledDuck;

    //[SerializeField]
    //private AssemblySO assembledDuckSO;


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
            Debug.Log("has duck");
            DucksSO outputDuckSO = GetOutputForInput(GetDuckObject().GetDucksSO());
            GetDuckObject().DestroySelf();
            //DuckObject.spawnDuckObject(outputDuckSO, this);

            //spawn function takes in duckSO but returns duckObject
            DuckObject.spawnDuckObject(outputDuckSO, this);
        }
        else
        {
            Debug.Log("no duck");
        }
    }

    //this function takes in a ducksSO, matches it to the input of one of the items in the assemblySOarray
    // and returns the output, a ducksSO
    private DucksSO GetOutputForInput(DucksSO inputDuckSO)
    {
        foreach (AssemblySO assemblySO in assembledDucksSOArray)
        {
            if (assemblySO.input == inputDuckSO)
            {
                return assemblySO.output;
            }
        }
        return null;
    }

    ////this function takes in an assemblySO object pair and returns the output
    //private DucksSO GetOutputForInput(AssemblySO inputDuckSO)
    //{

    //    return inputDuckSO.output;


    //}
}
