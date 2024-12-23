using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]

public class AssemblySO : ScriptableObject
{
    public DucksSO input;
    public DucksSO output;
    public int assemblyProgressMax;
}
