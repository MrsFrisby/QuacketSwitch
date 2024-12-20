using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckObject : MonoBehaviour
{
    [SerializeField] private DucksSO duckObjectSO;

    private Quacket quacket;

    public DucksSO GetDucksSO()
    {
        return duckObjectSO;
    }


    //the argument passed into this function is the new quacket parent that the duckObject is moving to
    public void SetQuacket(Quacket quacket)
    {
        //this.quacket refers to the current/orginal parent
        if (this.quacket != null)
        {
            this.quacket.ClearDuckObject();
        }

        //here we reset to the new parent
        this.quacket = quacket;

        if(quacket.HasDuckObject())
        {
            Debug.LogError("Quacket already has child duckObject");
        }


        quacket.SetDuckObject(this);

        //this is the position on the new parent object that the duck will move to
        transform.parent = quacket.GetDuckObjectFolllowTransform();
        transform.localPosition = Vector3.zero;
    }

    public Quacket GetQuacket()
    {
        return quacket;
    }
}
