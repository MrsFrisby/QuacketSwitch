using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPallet : BasePallet
{
    [SerializeField]
    private DucksSO ducksSO;

    //[SerializeField]
    //private Transform palletTopPoint;

    //[SerializeField] private Pallet secondPallet;
    //[SerializeField] private bool testing;

    //private DuckObject duckObject;


    //private void Update()
    //{
    //    if (testing && Input.GetKeyDown(KeyCode.T))
    //    {
    //        Debug.Log("PalletScript: T key pressed");
    //        if (duckObject != null)
    //        {
    //            duckObject.SetDuckObjectParent(secondPallet);
    //            //Debug.Log("Update:The parent of this " +duckObject.GetDucksSO().name + " is " + duckObject.GetPallet());
    //        }
    //    }
    //}

    public override void Interact(Player player)
    {
        //Debug.Log("I am interacting with a "+gameObject.transform.name);

        //if there isn't already a duck
        if (!HasDuckObject()) {

            //spawn a new duck
            Transform spawnPoint = GetDuckObjectFollowTransform();

            Transform duckTransform = Instantiate(ducksSO.prefab, spawnPoint);

           
            duckTransform.GetComponent<DuckObject>().SetDuckObjectParent(this);

            Debug.Log("I am spawning a " +duckTransform.GetComponent<DuckObject>().GetDucksSO().name);
        }
        else
        {
           
        }
    }

   
}
