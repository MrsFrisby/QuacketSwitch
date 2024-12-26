using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]

public class CorruptionSO : ScriptableObject
{
    public DucksSO input;
    public DucksSO output;
    public float corruptionTimerMax;
}
