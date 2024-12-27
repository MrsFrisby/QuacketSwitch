using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHoles : BasePallet
{
    public event EventHandler OnDuckSpawned;


    private float duckHoleTimer;
    private float duckHoleTimerMax = 1f;
    private int ducksSpawned;
    private int ducksSpawnedMax = 24;

    [SerializeField] private DucksSO ducksSO;

    private void Update()
    {
        //duckHoleTimer += Time.deltaTime;
        //if (duckHoleTimer > duckHoleTimerMax)
        //{
            
        //    duckHoleTimer = 0f;

        //    if (ducksSpawned < ducksSpawnedMax)
        //    {
        //        ducksSpawned++;
        //        OnDuckSpawned?.Invoke(this, EventArgs.Empty);
                
        //    }   
        //}
    }

    public override void Interact(Player player)
    {
        if (player.HasDuckObject())
        {
            player.GetDuckObject().DestroySelf();
            if (ducksSpawned < ducksSpawnedMax)
            {
                ducksSpawned++;
                OnDuckSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
