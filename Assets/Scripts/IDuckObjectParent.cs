using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDuckObjectParent 
{
    public Transform GetDuckObjectFollowTransform();

    public void SetDuckObject(DuckObject duckObject);

    public DuckObject GetDuckObject();

    public void ClearDuckObject();

    public bool HasDuckObject();
   
}
