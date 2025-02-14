using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteractable : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Interact with:"+gameObject.name);
    }
}
