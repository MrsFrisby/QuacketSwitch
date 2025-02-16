using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialInteractable : MonoBehaviour
{
    

    //[SerializeField]
    //private TextMeshProUGUI tutorialText;

    [SerializeField]
    private float timeToReset = 5f;

    private float timeSinceInteraction;


    private void Start()
    {
        //Hide();
    }

    private void Update()
    {
        timeSinceInteraction += Time.deltaTime;
        if (timeSinceInteraction >= timeToReset)
        {
            Hide();
        }
    }

    public void Interact()
    {
        Debug.Log("Interact with:"+gameObject.name);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        //timeSinceInteraction = 0;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
