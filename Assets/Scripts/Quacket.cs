using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacket : MonoBehaviour
{
    [SerializeField]
    private DucksSO ducksSO;

    [SerializeField]
    private Transform palletTopPoint;

    public void Interact()
    {
        Debug.Log("I am interacting with a "+gameObject.transform.name);
        Transform duckTransform = Instantiate(ducksSO.prefab,palletTopPoint);
        duckTransform.localPosition = Vector3.zero;

    }
}
