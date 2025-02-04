using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryFeedbackUI : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private TextMeshProUGUI feedbackText;

    [SerializeField]
    private Color completedColour;

    [SerializeField]
    private Color neutralColour;

    [SerializeField]
    private Color failColour;

    [SerializeField]
    private float timeToReset = 5f;

    private float timeSinceDelivery;

    private void Start()
    {
        AssemblyManager.Instance.OnProtocolCompleted += AssemblyManager_OnProtocolCompleted;
        AssemblyManager.Instance.OnProtocolFailed += AssemblyManager_OnProtocolFailed;
        AssemblyManager.Instance.OnProtocolSuccess += AssemblyManager_OnProtocolSuccess;

        backgroundImage.color = neutralColour;
        feedbackText.text = "AWAITING\nTRANSMISSION";
    }

    private void Update()
    {
        timeSinceDelivery += Time.deltaTime;
        if (timeSinceDelivery >= timeToReset)
        {
            backgroundImage.color = neutralColour;
            feedbackText.text = "AWAITING\nTRANSMISSION";
        }
    }



    private void AssemblyManager_OnProtocolSuccess(object sender, System.EventArgs e)
    {
        Debug.Log("DeliveryFeedbackUI:OnProtocolSuccess");
        backgroundImage.color = completedColour;
        feedbackText.text = "TRANSMISSION\nACCEPTED";
        timeSinceDelivery = 0f;
    }

    private void AssemblyManager_OnProtocolFailed(object sender, System.EventArgs e)
    {
        Debug.Log("DeliveryFeedbackUI:OnProtocolFailed");

        backgroundImage.color = failColour;
        feedbackText.text = "TRANSMISSION\nREJECTED";
        timeSinceDelivery = 0f;
    }

    private void AssemblyManager_OnProtocolCompleted(object sender, System.EventArgs e)
    {
        Debug.Log("DeliveryFeedbackUI:OnProtocolCompleted");
    }
}
