using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePallet : MonoBehaviour
{
   public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }
}
