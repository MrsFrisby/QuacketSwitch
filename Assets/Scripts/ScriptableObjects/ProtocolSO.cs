using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ProtocolSO : ScriptableObject
{
    public List<DucksSO> duckObjectSOList;
    public DucksSO ducksSO;
    public Sprite sprite;
    public string protocolName;
}
