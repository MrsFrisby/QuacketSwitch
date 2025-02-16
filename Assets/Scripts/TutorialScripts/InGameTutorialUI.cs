using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTutorialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject keyContainer;

    [SerializeField]
    private Player player;

    private void Update()
    {
        if (player.GetTutorialInteractable() !=null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        keyContainer.SetActive(true);
    }

    private void Hide()
    {
        keyContainer.SetActive(false);
    }
}
