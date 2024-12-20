using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckObject : MonoBehaviour
{
    [SerializeField] private DucksSO duckObjectSO;

    public DucksSO GetDucksSO()
    {
        return duckObjectSO;
    }
}
