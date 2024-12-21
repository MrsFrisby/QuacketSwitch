using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckObject : MonoBehaviour
{
    [SerializeField] private DucksSO duckObjectSO;

    private Pallet pallet;

    public DucksSO GetDucksSO()
    {
        return duckObjectSO;
    }


    //the argument passed into this function is the new pallet parent that the duckObject is moving to
    public void SetPallet(Pallet pallet)
    {
        //this.pallet refers to the current/orginal parent
        if (this.pallet != null)
        {
            this.pallet.ClearDuckObject();
        }

        //here we reset to the new parent
        this.pallet = pallet;

        if(pallet.HasDuckObject())
        {
            Debug.LogError("pallet already has child duckObject");
        }


        pallet.SetDuckObject(this);

        //this is the position on the new parent object that the duck will move to
        transform.parent = pallet.GetDuckObjectFolllowTransform();
        transform.localPosition = Vector3.zero;
    }

    public Pallet GetPallet()
    {
        return pallet;
    }
}
