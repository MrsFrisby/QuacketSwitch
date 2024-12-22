using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : BasePallet
{

    [SerializeField]
    private DucksSO ducksSO;

    //[SerializeField]
    //private Transform palletTopPoint;

    //private DuckObject duckObject;


    public override void Interact(Player player)
    {

        if (!HasDuckObject())
        {
            Transform duckTransform = Instantiate(ducksSO.prefab);
            duckTransform.GetComponent<DuckObject>().SetDuckObjectParent(player);
        }
        else
        {
           // duckObject.SetDuckObjectParent(player);
        }
    }

    //public Transform GetDuckObjectFollowTransform()
    //{
    //    return palletTopPoint;
    //}


    //public void SetDuckObject(DuckObject duckObject)
    //{
    //    this.duckObject = duckObject;
    //}

    //public DuckObject GetDuckObject()
    //{
    //    return duckObject;
    //}

    //public void ClearDuckObject()
    //{
    //    duckObject = null;
    //}

    //public bool HasDuckObject()
    //{
    //    return duckObject != null;
    //}
}
