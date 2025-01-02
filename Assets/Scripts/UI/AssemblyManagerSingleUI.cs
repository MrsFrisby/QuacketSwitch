using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyManagerSingleUI : MonoBehaviour
{
    [SerializeField]//UI Text class from TextMeshPro
    private TextMeshProUGUI protocolNameText;

    [SerializeField]
    private Transform iconContainer;
    [SerializeField]
    private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }


    public void SetProtocolSO(ProtocolSO protocolSO)
    {
        protocolNameText.text = protocolSO.protocolName;

        foreach (Transform child in iconContainer)
        {//clean up - destroy all child objects of IconContainer
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (DucksSO ducksSO in protocolSO.duckObjectSOList)
        {
            //Debug.Log(ducksSO.sprite.name);
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = ducksSO.sprite;
        }
    }
}
