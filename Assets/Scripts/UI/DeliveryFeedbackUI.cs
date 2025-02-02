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
    private Color failColour;

    private void Start()
    {
        AssemblyManager.Instance.OnProtocolCompleted += AssemblyManager_OnProtocolCompleted;
        AssemblyManager.Instance.OnProtocolFailed += AssemblyManager_OnProtocolFailed;
    }

    private void AssemblyManager_OnProtocolFailed(object sender, System.EventArgs e)
    {
        backgroundImage.color = failColour;
        feedbackText.text = "TRANSMISSION\nREJECTED";
    }

    private void AssemblyManager_OnProtocolCompleted(object sender, System.EventArgs e)
    {
        backgroundImage.color = completedColour;
        feedbackText.text = "TRANSMISSION\nDELIVERED";
    }
}
