using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDeliverUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssemblyManager.Instance.OnProtocolDelivered += AssemblyManager_OnProtocolDelivered;
        Hide();
    }

    private void AssemblyManager_OnProtocolDelivered(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
