using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDeliverUI : MonoBehaviour
{
    private float showTimer;
    private float showTimerMax = 5f;



    // Start is called before the first frame update
    void Start()
    {
        showTimer = 0;
        AssemblyManager.Instance.OnProtocolDelivered += AssemblyManager_OnProtocolDelivered;
        Hide();
    }

    private void Update()
    {
        showTimer += Time.deltaTime;
        if (showTimer > showTimerMax)
        {
            Hide();
        }
    }

    private void AssemblyManager_OnProtocolDelivered(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        showTimer = 0f;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
