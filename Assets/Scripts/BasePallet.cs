using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePallet : MonoBehaviour, IDuckObjectParent
{
    [SerializeField]
    private Transform palletTopPoint;
    private DuckObject duckObject;


    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
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
