using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerPallet : BasePallet, IHasProgress

{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler <OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
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

        state = State.Idle;
    }

    private void Update()
    {
        if (HasDuckObject())
        {
            switch (state)
            {
                case State.Idle:
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    break;
                case State.Green:
                    cookingTimer += Time.deltaTime;
                    //Debug.Log("Timer: " + cookingTimer);

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = cookingTimer / cookingSO.cookingTimerMax
                    });



                    if (cookingTimer > cookingSO.cookingTimerMax)
                    {
                        //cooked
                        GetDuckObject().DestroySelf();
                        DuckObject.spawnDuckObject(cookingSO.output, this);
                        state = State.Red;
                        corruptionTimer = 0f;
                        corruptionSO = GetCorruptionSOWithInput(GetDuckObject().GetDucksSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
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

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = corruptionTimer / corruptionSO.corruptionTimerMax
                    });


                    if (corruptionTimer > corruptionSO.corruptionTimerMax)
                    {//corrupt
                        GetDuckObject().DestroySelf();
                        DuckObject.spawnDuckObject(corruptionSO.output, this);
                        state = State.Corrupt;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Corrupt:    
                    break;

            }
        }
    }



    public override void Interact(Player player)
    {
        Debug.Log("CookerPallet Interact)");
        if (!HasDuckObject())
        {//no duck already on pallet

            if (player.HasDuckObject())
            {//no duck already but player is carrying a duck
                Debug.Log("carrying a " + player.GetDuckObject().GetDucksSO());

                if (HasMatchwithSOCookingInput(player.GetDuckObject().GetDucksSO()))
                {//duck dropped matches CookingSO.input duck object within pallet's array

                    //when E is pressed the duck is parented to this pallet

                    player.GetDuckObject().SetDuckObjectParent(this);
                    Debug.Log("This pallet now has a " + GetDuckObject().name);
                    cookingSO = GetCookingSOWithInput(GetDuckObject().GetDucksSO());
                    state = State.Green;
                    cookingTimer = 0f;


                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = cookingTimer / cookingSO.cookingTimerMax
                    });
                }
                else
                {
                    Debug.Log("No matching. this is a " + player.GetDuckObject().GetDucksSO());
                }
            }

            else
            {//no duck on pallet and player not carrying a duck
                Debug.Log("Can't do anything, no ducks!");
            }
        }
        else
        {//duck already on pallet
            if (player.HasDuckObject())
            {//player already has a duck so can't pick up another
                Debug.Log("Interact:player is already carrying a" + player.GetDuckObject().GetDucksSO());
            }
            else
            { //if player empty handed give duck to player
                Debug.Log("Interact:player should be able to pick up corrupt duck but can't??");
                GetDuckObject().SetDuckObjectParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
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
