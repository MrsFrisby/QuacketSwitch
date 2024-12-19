using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacket : MonoBehaviour
{
    [SerializeField]
    private Transform acidDuck;

    [SerializeField]
    private Transform palletTopPoint;

    public void Interact()
    {
        Debug.Log("I am interacting with a "+gameObject.transform.name);
        Transform acidDuckTransform = Instantiate(acidDuck,palletTopPoint);
        acidDuckTransform.localPosition = Vector3.zero;

    }
}
