using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPallet : BasePallet
{ 

    [SerializeField] private SpawnPalletDuckListSO spawnPalletDuckListSO;

    public override void Interact(Player player)
    {
        ////if there isn't already a duck
        //if (!HasDuckObject()) {

        //    //spawn a new random quacket duck parented to the pallet

        //    DucksSO ducksSO = spawnPalletDuckListSO.spawnPalletDuckSOList[UnityEngine.Random.Range(0, spawnPalletDuckListSO.spawnPalletDuckSOList.Count)];

        //    //DucksSO ducksSO = protocolListSO.protocolSOList[UnityEngine.Random.Range(0, protocolListSO.protocolSOList.Count)];

        //    Transform spawnPoint = GetDuckObjectFollowTransform();

        //    Transform duckTransform = Instantiate(ducksSO.prefab, spawnPoint);

        //    duckTransform.GetComponent<DuckObject>().SetDuckObjectParent(this);

        //    Debug.Log("I am spawning a " +duckTransform.GetComponent<DuckObject>().GetDucksSO().name);
        //}
        //else
        //{
        //    Debug.Log("Duck already on pallet");
        //}

        
    
        //if the player is not already carrying something
        if (!HasDuckObject())
        {

            DucksSO ducksSO = spawnPalletDuckListSO.spawnPalletDuckSOList[UnityEngine.Random.Range(0, spawnPalletDuckListSO.spawnPalletDuckSOList.Count)];

            DuckObject.spawnDuckObject(ducksSO, player);

            
        }
        else
        {
            Debug.Log("The player already has a duck");
        }
    
}
}
