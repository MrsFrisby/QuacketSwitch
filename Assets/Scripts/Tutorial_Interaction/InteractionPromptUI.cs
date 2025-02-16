//https://www.youtube.com/watch?v=THmW4YolDok
//Unity Interaction Tutorial | How To Interact With Any Game Object (Open Chests & Doors etc)
//DanPos 2022


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    //private Camera mainCamera;

    //canvas with background image
    [SerializeField]
    public GameObject uiPanel;

    //text
    [SerializeField]
    private TextMeshProUGUI tutorialPromptText;

    //image container for icon sprite
    [SerializeField]
    private Image tutorialIconImage;

    public bool IsDisplayed = false;


    private void Start()
    {
        //mainCamera = Camera.main;
        uiPanel.SetActive(false);
    }

    //function called by interactor script, supplies string and sprite as arguments
    public void SetUp(string promptText, Sprite iconImage)
    {
        tutorialPromptText.text = promptText;
        tutorialIconImage.sprite = iconImage;
        uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    { 
        uiPanel.SetActive(false);
        IsDisplayed = false;
    }

    //private void LateUpdate()
    //{
    //    var rotation = mainCamera.transform.rotation;
    //    transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    //}
}
