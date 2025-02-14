using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPopUp : MonoBehaviour
{
    [SerializeField]
    private Image background;

    [SerializeField]
    private TextMeshProUGUI instructionText;

    private void Start()
    {
        Setup("Hello World!");
    }

    private void Setup(string text)
    {
        instructionText.SetText(text);
    }
}
