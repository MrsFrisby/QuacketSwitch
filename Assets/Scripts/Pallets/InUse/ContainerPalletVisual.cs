using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPalletVisual : MonoBehaviour
{

    //this is a script to play an animation when a pallet is interacted with

    [SerializeField] private Container containerPallet;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerPallet.OnPlayerGrabbedObject += ContainerPallet_OnPlayerGrabbedObject;

    }

    private void ContainerPallet_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        //animator.setTrigger("animationName");
    }
}
