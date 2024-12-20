using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacket : MonoBehaviour
{
    [SerializeField]
    private DucksSO ducksSO;

    [SerializeField]
    private Transform palletTopPoint;

    [SerializeField] private Quacket secondQuacket;
    [SerializeField] private bool testing;

    private DuckObject duckObject;


    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("QuacketScript: T key pressed");
            if (duckObject != null)
            {
                duckObject.SetQuacket(secondQuacket);
                //Debug.Log("Update:The parent of this " +duckObject.GetDucksSO().name + " is " + duckObject.GetQuacket());
            }
        }
    }

    public void Interact()
    {
        //Debug.Log("I am interacting with a "+gameObject.transform.name);

        //if there isn't already a duck
        if (duckObject == null) {

            //spawn a new duck
            Transform duckTransform = Instantiate(ducksSO.prefab, palletTopPoint);
            //get the information about the spawned duck
            duckTransform.GetComponent<DuckObject>().SetQuacket(this);

            //duckTransform.localPosition = Vector3.zero;
            //duckObject = duckTransform.GetComponent<DuckObject>();

            //Debug.Log("I am spawning a " +duckTransform.GetComponent<DuckObject>().GetDucksSO().name);
        }
        else
        {
            Debug.Log("Interact:This is a "+duckObject.GetDucksSO().name+" on this "+duckObject.GetQuacket());
        }
    }

    public Transform GetDuckObjectFolllowTransform()
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
