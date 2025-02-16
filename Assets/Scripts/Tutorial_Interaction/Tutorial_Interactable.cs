//https://www.youtube.com/watch?v=THmW4YolDok
//Unity Interaction Tutorial | How To Interact With Any Game Object (Open Chests & Doors etc)
//DanPos 2022

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Interactable : MonoBehaviour, I_Interactable
{
    [SerializeField]
    private string prompt;

    [SerializeField]
    private Sprite iconSprite;

    //text
    [SerializeField]
    private string promptText2;

    //image container for icon sprite
    [SerializeField]
    private Sprite iconSprite2;

    public string InteractionPrompt => prompt;

    public Sprite InteractionSprite => iconSprite;

    public bool TutorialInteract(Interactor interactor)
    {
        Debug.Log("Tutorial Interactable: "+ prompt);
        var tutorialCanvas = interactor.GetComponent<Interactor>();
        //Debug.Log(tutorialCanvas);
        //Debug.Log(tutorialCanvas.interactionPromptUI.uiPanel);
        //tutorialCanvas.interactionPromptUI.uiPanel.SetActive(false);
        //tutorialCanvas.interactionPromptUI.IsDisplayed = false;
        tutorialCanvas.interactionPromptUI.SetUp(promptText2, iconSprite2);
        return true;
    }
}
