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

    //text
    [SerializeField]
    private string promptText3;

    //image container for icon sprite
    [SerializeField]
    private Sprite iconSprite3;

    private float wrongDuckTimer;
    private float wrongDuckTimerMax = 5f;

    public string InteractionPrompt => prompt;

    public Sprite InteractionSprite => iconSprite;
    private bool correctDuck;

    private bool duckAssembled;

    private void Start()
    {
        AssemblyPalletDuckHoles.onDuckAssembled += AssemblyPalletDuckHoles_onDuckAssembled;
        AssemblyManager.Instance.OnProtocolSuccess += AssemblyManager_OnProtocolSuccess;
        AssemblyPalletDuckHoles.OnWrongDuck += AssemblyPalletDuckHoles_OnWrongDuck;
        duckAssembled = false;
        correctDuck = true;
        wrongDuckTimer = 0;
    }


    private void Update()
    {
        wrongDuckTimer += Time.deltaTime;
        if (wrongDuckTimer > wrongDuckTimerMax)
        {
            correctDuck = true;
        }
    }
    private void AssemblyPalletDuckHoles_OnWrongDuck(object sender, System.EventArgs e)
    {
        wrongDuckTimer = 0;
        correctDuck = false;
    }

    private void AssemblyManager_OnProtocolSuccess(object sender, System.EventArgs e)
    {
        duckAssembled = false;
    }

    private void AssemblyPalletDuckHoles_onDuckAssembled(object sender, System.EventArgs e)
    {
        duckAssembled = true;
    }

    public bool TutorialInteract(Interactor interactor)
    {
        if (!duckAssembled)
        {
            if (!correctDuck)
            {
                var tutorialCanvas3 = interactor.GetComponent<Interactor>();
                tutorialCanvas3.interactionPromptUI.SetUp(promptText3, iconSprite3);
                return true;
            }
            else
            {
                //Debug.Log("Tutorial Interactable: "+ prompt);
                var tutorialCanvas2 = interactor.GetComponent<Interactor>();
                //Debug.Log(tutorialCanvas);
                //Debug.Log(tutorialCanvas.interactionPromptUI.uiPanel);
                //tutorialCanvas.interactionPromptUI.uiPanel.SetActive(false);
                //tutorialCanvas.interactionPromptUI.IsDisplayed = false;
                tutorialCanvas2.interactionPromptUI.SetUp(promptText2, iconSprite2);
                return true;
            }            
        }
        else return false;
    }
}
