using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHoles2x2 : BasePallet
{
    public event EventHandler OnDuckSpawned;

    public event EventHandler OnDestroyLast;


    private float duckHoleTimer;
    private float duckHoleTimerMax = 320f;
    private int ducksSpawned;
    private int ducksSpawnedMax = 4;

    public List<DucksSO> duckObjectSOList;

    [SerializeField] private DucksSO ducksSO;

    [SerializeField] private List<DucksSO> validDucksSOList;

    private DucksSO playerDuckSO;

    private void Awake()
    {
        duckObjectSOList = new List<DucksSO>();
    }


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
            if (HasMatchWithValidDuckSOList(playerDuckSO))
            {
                 if(TryAddDucktoList(playerDuckSO))
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
        else
        {
            Debug.Log("Wrong Duck SO");
        }
    }

    private bool HasMatchWithValidDuckSOList(DucksSO playerDuckSO)
    {
        if (validDucksSOList.Contains(playerDuckSO))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool TryAddDucktoList(DucksSO ducksSO)
    {
        if (duckObjectSOList.Contains(ducksSO))
        {//this duck has already been added
            return false;
        }
        else
        {
            duckObjectSOList.Add(ducksSO);
            return true;
        }
    }

    public List<DucksSO> GetDucksSOList()
    {
        return duckObjectSOList;
    }

}
