using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePallet : MonoBehaviour, IDuckObjectParent
{

    public static event EventHandler OnAnyDuckDropHere;

    public static void ResetStaticData()
    {
        OnAnyDuckDropHere = null; 
    }

    [SerializeField]
    private Transform palletTopPoint;
    private DuckObject duckObject;


    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate()");
    }

    public Transform GetDuckObjectFollowTransform()
    {
        return palletTopPoint;
    }


    public void SetDuckObject(DuckObject duckObject)
    {
        this.duckObject = duckObject;

        if (duckObject != null)
        {
            OnAnyDuckDropHere?.Invoke(this, EventArgs.Empty);
        }
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
