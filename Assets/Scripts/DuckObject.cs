using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckObject : MonoBehaviour
{
    [SerializeField] private DucksSO duckObjectSO;

    private IDuckObjectParent duckObjectParent;

    public DucksSO GetDucksSO()
    {
        return duckObjectSO;
    }


    //the argument passed into this function is the new pallet parent that the duckObject is moving to
    public void SetDuckObjectParent(IDuckObjectParent duckObjectParent)
    {
        //this.pallet refers to the current/orginal parent
        if (this.duckObjectParent != null)
        {
            this.duckObjectParent.ClearDuckObject();
        }

        //here we reset to the new parent
        this.duckObjectParent = duckObjectParent;

        if(duckObjectParent.HasDuckObject())
        {
            Debug.LogError("duckObjcetParent already has child duckObject");
        }


        duckObjectParent.SetDuckObject(this);

        //this is the position on the new parent object that the duck will move to
        transform.parent = duckObjectParent.GetDuckObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IDuckObjectParent GetDuckObjectParent()
    {
        return duckObjectParent;
    }

    public void DestroySelf()
    {
        duckObjectParent.ClearDuckObject();

        Destroy(gameObject);
    }

}
