using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacket : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("You are near a "+gameObject.transform.name);
    }
}
