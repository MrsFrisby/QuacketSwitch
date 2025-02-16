//https://www.youtube.com/watch?v=THmW4YolDok
//Unity Interaction Tutorial | How To Interact With Any Game Object (Open Chests & Doors etc)
//DanPos 2022

//This script goes on the Player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    //Interaction SEtup
    [SerializeField]
    private Player player;

    [SerializeField]
    private Transform interactionPoint;

    [SerializeField]
    private float interactionPointRadius;

    [SerializeField]
    private LayerMask interactableMask;


    //Canvas with interactionPromptUI script
    [SerializeField]
    public InteractionPromptUI interactionPromptUI;

    //Canvas Image
    //[SerializeField]
    //private Image interactionIcon;

    private Collider[] colliders = new Collider[3];

    //[SerializeField]
    private int numFound;

    private I_Interactable interactable;

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<I_Interactable>();
            if (interactable != null)
            {
                // Debug.Log("Interactable not null");
                if (! interactionPromptUI.IsDisplayed)
                {
                    interactionPromptUI.SetUp(interactable.InteractionPrompt, interactable.InteractionSprite);
                }

                if (player.isEPressed)
                {
                    Debug.Log("E pressed and interactable not null");
                    interactable.TutorialInteract(this);
                }
            }


            

            //if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
            //{
            //    interactable.TutorialInteract(this);
            //}
        }
        else
        {
            if (interactable != null)
            {
                interactable = null;
            }
            if (interactionPromptUI.IsDisplayed)
            {
                interactionPromptUI.Close();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);

    }
}
