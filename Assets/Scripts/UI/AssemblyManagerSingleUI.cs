using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssemblyManagerSingleUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI protocolNameText;
    [SerializeField]
    private Transform iconContainer;
    [SerializeField]
    private Transform protocolTemplate;


    public void SetProtocolSO(ProtocolSO protocolSO)
    {
        protocolNameText.text = protocolSO.protocolName;

        foreach (Transform child in iconContainer)
        {
            if (child == protocolTemplate) continue;
            Destroy(child.gameObject);
        }
    }
}
