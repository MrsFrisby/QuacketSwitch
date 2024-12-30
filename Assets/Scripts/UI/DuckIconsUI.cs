using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckIconsUI : MonoBehaviour
{
    [SerializeField] private DuckObject duckObject;
    //[SerializeField] private Player player;

    //private Player player;
    private IDuckObjectParent duckObjectParent;

    [SerializeField] public GameObject iconTemplate;
    [SerializeField] private Image image;



    private void Awake()
    {
        iconTemplate.gameObject.SetActive(true);
        
        //Debug.Log("DuckIconsUI:DuckObjectParent: " + duckObjectParent);
        //player = (Player)duckObjectParent;
        
    }


    private void Start()
    {
        //player.gameInput.OnInspectAction += player_OnInspectAction;
        //player.gameInput.OnInspectCancelAction += player_OnInspectCancelAction;
    }

    //private void player_OnInspectCancelAction(object sender, EventArgs e)
    //{
    //    VisualOff();
    //}



    //private void player_OnInspectAction(object sender, EventArgs e)
    //{
    //    VisualOn();
    //}


    private void Update()
    {
        //duckObjectParent = duckObject.GetDuckObjectParent();
        //Debug.Log("DuckIconsUI:DuckObjectParent: " + duckObjectParent);
    }

    private void VisualOff()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void VisualOn()
    {
        iconTemplate.gameObject.SetActive(true);
    }

    //Not working
    //private void Update()
    //{
    //    //if (player.HasDuckObject())
    //    //{
    //        //duckObject = player.GetDuckObject();
    //        if (duckObject.displayIconUI)
    //        {
    //            //Debug.Log("DuckIconsUI: set active to true");
    //            //GetVisual(duckObject.GetDucksSO());
    //            iconTemplate.SetActive(true);
    //        }
    //        else
    //        {
    //            iconTemplate.SetActive(false);
    //        }
    //    //}
    //}

    //private void GetVisual(DucksSO ducksSO)
    //{

    //    image.sprite = ducksSO.sprite;
    //}
}
