using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHoles : BasePallet
{
    public event EventHandler OnDuckSpawned;

    public event EventHandler OnDestroyLast;


    private float duckHoleTimer;
    private float duckHoleTimerMax = 30f;
    private int ducksSpawned;
    private int ducksSpawnedMax = 24;

    [SerializeField] private DucksSO ducksSO;

    private DucksSO playerDuckSO;

    private void Update()
    {
        duckHoleTimer += Time.deltaTime;
        //Debug.Log(duckHoleTimer);
        if (duckHoleTimer > duckHoleTimerMax)
        {
            duckHoleTimer = 0f;
            ducksSpawned--;
            OnDestroyLast?.Invoke(this, EventArgs.Empty);
            Debug.Log("Reset Timer");
        }
    }

    public override void Interact(Player player)
    {
        if (player.HasDuckObject())
        {
            playerDuckSO = player.GetDuckObject().GetDucksSO();
            if (HasMatchWithDuckSO(playerDuckSO))
            {
                player.GetDuckObject().DestroySelf();
                if (ducksSpawned < ducksSpawnedMax)
                {
                    ducksSpawned++;
                    OnDuckSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        else
        {
            Debug.Log("Wrong Duck SO");
        }
    }

    private bool HasMatchWithDuckSO(DucksSO playerDuckSO)
    {
        if (playerDuckSO == ducksSO)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
