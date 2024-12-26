using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerPallet : BasePallet

{
    private enum State
    {
        Idle,
        Green,
        Acid,
        Yellow,
        Orange,
        Red,
        Corrupt
    }


    [SerializeField]
    private CookingSO[] CookingSOArray;

    [SerializeField]
    private CorruptionSO[] CorruptionSOArray;


    private State state;
    private float cookingTimer;
    private float corruptionTimer;
    private CookingSO cookingSO;
    private CorruptionSO corruptionSO;

    // //coroutine for timer
    //private IEnumerator HandleCookingTimer()
    //{
    //    yield return new WaitForSeconds(3f);
    //}

    //private void Start()
    //{
    //    StartCoroutine(HandleCookingTimer());
    //}

    private void Start()
    {

        state = State.Green;
    }

    private void Update()
    {
        if (HasDuckObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Green:
                    cookingTimer += Time.deltaTime;
                    Debug.Log("Timer: " + cookingTimer);
                    if (cookingTimer > cookingSO.cookingTimerMax)
                    {
                        //cooked
                        GetDuckObject().DestroySelf();
                        DuckObject.spawnDuckObject(cookingSO.output, this);
                        state = State.Red;
                        corruptionTimer = 0f;
                        corruptionSO = GetCorruptionSOWithInput(GetDuckObject().GetDucksSO());
                    }
                    break;
                case State.Acid:
                    break;
                case State.Yellow:
                    
                    break;
                case State.Orange:
                    break;
                case State.Red:
                    corruptionTimer += Time.deltaTime;
                    Debug.Log("CorruptTimer: " + cookingTimer);
                    if (corruptionTimer > corruptionSO.corruptionTimerMax)
                    {
                        //cooked
                        GetDuckObject().DestroySelf();
                        DuckObject.spawnDuckObject(corruptionSO.output, this);
                        state = State.Corrupt;
                    }
                    break;
                case State.Corrupt:
                    
                    break;

            }
        }
    }



    public override void Interact(Player player)
    {
        if (!HasDuckObject())
        {//no duck already on pallet
            if (player.HasDuckObject())
                
            {//if player is carrying a duck
                Debug.Log("carrying a " + player.GetDuckObject().GetDucksSO());
                if (HasMatchwithSOCookingInput(player.GetDuckObject().GetDucksSO()))
                {//if duck dropped matches CookingSO.input duck object within pallet's array

                    //when E is pressed the duck is parented to this pallet
                    player.GetDuckObject().SetDuckObjectParent(this);
                    cookingSO = GetCookingSOWithInput(GetDuckObject().GetDucksSO());
                    state = State.Green;
                    cookingTimer = 0f;
                }
                else
                {
                    Debug.Log("No matching. this is a "+ player.GetDuckObject().GetDucksSO());
                }
            }
            else
            {//duck already on pallet
                if (player.HasDuckObject())
                {//player already has a duck so can't pick up another

                }
                else
                { //if player empty handed give duck to player
                    GetDuckObject().SetDuckObjectParent(player);
                }
            }
        }
    }

    private bool HasMatchwithSOCookingInput(DucksSO inputDuckSO)
    {
        CookingSO cookingSO = GetCookingSOWithInput(inputDuckSO);
        if(cookingSO != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //this function takes in a ducksSO, matches it to the input of one of the items in the assemblySOarray
    // and returns the output, a ducksSO
    private DucksSO GetOutputForInput(DucksSO inputDuckSO)
    {
        CookingSO cookingSO = GetCookingSOWithInput(inputDuckSO);
        if (cookingSO != null)
        {
            return cookingSO.output;
        }
        else
        {
            return null;
        }
    }

    private CookingSO GetCookingSOWithInput(DucksSO inputDucksSO)

    {
        foreach (CookingSO cookingSO in CookingSOArray)
        {
            if (cookingSO.input == inputDucksSO)
            {
                return cookingSO;
            }
        }
        return null;
    }

    private CorruptionSO GetCorruptionSOWithInput(DucksSO inputDucksSO)

    {
        foreach (CorruptionSO corruptionSO in CorruptionSOArray)
        {
            if (corruptionSO.input == inputDucksSO)
            {
                return corruptionSO;
            }
        }
        return null;
    }

}
