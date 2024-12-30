using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembledDuckObject : DuckObject
{
    [SerializeField]
    private List<DucksSO> duckSOList;

    //[Serializable]
    //public struct duckSO_GameObject
    //{ //struct used instead of class to store data without logic
    //    public DucksSO ducksSO;
    //    public GameObject duckGameObject;
    //}

    //[SerializeField]
    //private List<duckSO_GameObject> duckSO_GameObjectList;

    public List<DucksSO> GetDucksSOList()
    {
        return duckSOList;
    }
}
