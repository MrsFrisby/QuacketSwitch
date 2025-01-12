using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyPallet : BasePallet, IHasProgress

{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;

    }

    [SerializeField]
    private AssemblySO[] assembledDucksSOArray;

    //[SerializeField]
    //private DucksSO assembledDuck;

    //[SerializeField]
    //private AssemblySO assembledDuckSO;

    private int assemblyProgress;


    public override void Interact(Player player)
    {
        Debug.Log("E pressed");

        if (!HasDuckObject())
        {//no duck already on pallet
            if (player.HasDuckObject())
            {//if player is carrying a duck 
                if (HasMatchwithSOAssemblyInput(player.GetDuckObject().GetDucksSO()))
                {//if duck dropped has AssemblySO within pallet's array

                    //when E is pressed the duck is parented to this pallet
                    player.GetDuckObject().SetDuckObjectParent(this);
                    assemblyProgress = 0;

                    AssemblySO assemblySO = GetAssemblySOWithInput(GetDuckObject().GetDucksSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)assemblyProgress / assemblySO.assemblyProgressMax
                    });
                }
            }
                
            else
            {//can't do anything, no ducks!

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



    public override void InteractAlternate(Player player)
    {
        Debug.Log("F pressed");
       
        //duck already on pallet
        if (HasDuckObject() && HasMatchwithSOAssemblyInput(GetDuckObject().GetDucksSO()))
        {//duck already on pallet and has match with available assemblySO

            assemblyProgress++;

            AssemblySO assemblySO = GetAssemblySOWithInput(GetDuckObject().GetDucksSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)assemblyProgress / assemblySO.assemblyProgressMax
            });
            //Debug.Log("has duck");
            if (assemblyProgress >= assemblySO.assemblyProgressMax)
            {//if local pallet count of number of times F key pressed >= max assembly count for assemblySO

                DucksSO outputDuckSO = GetOutputForInput(GetDuckObject().GetDucksSO());
                GetDuckObject().DestroySelf();
                //spawn function takes in duckSO but returns duckObject
                DuckObject.spawnDuckObject(outputDuckSO, this);
            }          
        }
        else
        {
            Debug.Log("no duck");
        }
    }


    private bool HasMatchwithSOAssemblyInput(DucksSO inputDuckSO)
    {
        AssemblySO assemblySO = GetAssemblySOWithInput(inputDuckSO);
        return assemblySO != null;
    }

    //this function takes in a ducksSO, matches it to the input of one of the items in the assemblySOarray
    // and returns the output, a ducksSO
    private DucksSO GetOutputForInput(DucksSO inputDuckSO)
    {
        AssemblySO assemblySO = GetAssemblySOWithInput(inputDuckSO);
        if (assemblySO != null)
        {
            return assemblySO.output;
        }
        else
        {
            return null;
        }     
    }

    private AssemblySO GetAssemblySOWithInput(DucksSO inputDucksSO)

    {
        foreach (AssemblySO assemblySO in assembledDucksSOArray)
        {
            if (assemblySO.input == inputDucksSO)
            {
                return assemblySO;
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
