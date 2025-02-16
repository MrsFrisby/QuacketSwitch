//https://www.youtube.com/watch?v=THmW4YolDok
//Unity Interaction Tutorial | How To Interact With Any Game Object (Open Chests & Doors etc)
//DanPos 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface I_Interactable 
{
    public string InteractionPrompt { get; }

    public Sprite InteractionSprite { get; }

    public bool TutorialInteract(Interactor interactor);
}
