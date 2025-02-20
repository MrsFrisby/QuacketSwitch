using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManagerUI : MonoBehaviour
{
    [SerializeField]
    private Transform container;

    [SerializeField]
    private Transform protocolTemplate;


    private void Awake()
    {
        protocolTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        AssemblyManager.Instance.OnProtocolSpawned += AssemblyManager_OnProtocolSpawned;
        AssemblyManager.Instance.OnProtocolCompleted += AssemblyManager_OnProtocolCompleted;

        UpdateVisual();
        Debug.Log("AM:Start");
    }

    private void AssemblyManager_OnProtocolCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void AssemblyManager_OnProtocolSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Debug.Log("AM:UpdateVisual");
        foreach (Transform child in container)
        {
            if (child == protocolTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (ProtocolSO protocolSO in AssemblyManager.Instance.GetWaitingProtocolSOList())
        {
            Debug.Log("AM:UpdateVisual foreach protocol");
            Transform protocolTransform = Instantiate(protocolTemplate, container);
            protocolTransform.gameObject.SetActive(true);
            protocolTransform.GetComponent<AssemblyManagerSingleUI>().SetProtocolSO(protocolSO);

        }
    }
}
