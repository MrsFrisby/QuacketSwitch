using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembledDuckObject : DuckObject
{
    [SerializeField]
    private List<DucksSO> duckSOList;

    public List<DucksSO> GetDucksSOList()
    {
        return duckSOList;
    }
}
