using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : BasePallet, IDuckObjectParent
{
    [SerializeField]
    private DucksSO ducksSO;

    [SerializeField]
    private Transform palletTopPoint;

    //[SerializeField] private Pallet secondPallet;
    //[SerializeField] private bool testing;

    private DuckObject duckObject;


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
        if (duckObject == null) {

            //spawn a new duck
            Transform duckTransform = Instantiate(ducksSO.prefab, palletTopPoint);
            //get the information about the spawned duck
            duckTransform.GetComponent<DuckObject>().SetDuckObjectParent(this);

            //duckTransform.localPosition = Vector3.zero;
            //duckObject = duckTransform.GetComponent<DuckObject>();

            //Debug.Log("I am spawning a " +duckTransform.GetComponent<DuckObject>().GetDucksSO().name);
        }
        else
        {
            Debug.Log("Interact:This is a "+duckObject.GetDucksSO().name+" on this "+duckObject.GetDuckObjectParent());
            //not sure if I want to do this 
            duckObject.SetDuckObjectParent(player);
        }
    }

    public Transform GetDuckObjectFollowTransform()
    {
        return palletTopPoint;
    }


    public void SetDuckObject(DuckObject duckObject)
    {
        this.duckObject = duckObject;
    }

    public DuckObject GetDuckObject()
    {
        return duckObject;
    }

    public void ClearDuckObject()
    {
        duckObject = null;
    }

    public bool HasDuckObject()
    {
        return duckObject != null;
    }
}
