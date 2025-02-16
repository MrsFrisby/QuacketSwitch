//https://www.youtube.com/watch?v=THmW4YolDok
//Unity Interaction Tutorial | How To Interact With Any Game Object (Open Chests & Doors etc)
//DanPos 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Interactable : MonoBehaviour, I_Interactable
{
    [SerializeField]
    private string prompt;

    [SerializeField]
    private Sprite iconSprite;

    public string InteractionPrompt => prompt;

    public Sprite InteractionSprite => iconSprite;

    public bool TutorialInteract(Interactor interactor)
    {
        Debug.Log("Tutorial Interact: "+ prompt);
        return true;
    }
}
