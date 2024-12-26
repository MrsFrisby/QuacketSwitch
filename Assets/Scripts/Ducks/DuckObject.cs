using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckObject : MonoBehaviour
{
    [SerializeField] private DucksSO duckObjectSO;

    private IDuckObjectParent duckObjectParent;


    //how is the SO set? - via Serialized field for object
    public DucksSO GetDucksSO()
    {
        return duckObjectSO;
    }


    //the argument passed into this function is the new pallet parent that the duckObject is moving to
    public void SetDuckObjectParent(IDuckObjectParent duckObjectParent)
    {
        //clear the duck so it is no longer parented to the previous parent
        //this refers to the pallet, not the duck
        if (this.duckObjectParent != null)
        {
            this.duckObjectParent.ClearDuckObject();
        }

        if (!duckObjectParent.HasDuckObject())
        {
            //here we reset to the new parent
            this.duckObjectParent = duckObjectParent;
            Debug.Log("duckObjectParent does not already have child");
            duckObjectParent.SetDuckObject(this);
            //this is the position on the new parent object that the duck will move to
            transform.parent = duckObjectParent.GetDuckObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("duckObjectParent already has child duckObject");
        }
        
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

    public static DuckObject spawnDuckObject(DucksSO duckSO, IDuckObjectParent duckObjectParent)
    {
        Transform duckTransform = Instantiate(duckSO.prefab);
        DuckObject duckObject = duckTransform.GetComponent<DuckObject>();
        duckObject.SetDuckObjectParent(duckObjectParent);
        return duckObject;
    }

}
