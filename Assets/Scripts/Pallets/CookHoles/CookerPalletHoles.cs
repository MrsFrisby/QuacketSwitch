using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerPalletHoles : BasePallet, IHasProgress

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


    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler <OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    /*states
     * Idle: empty duck holes/idle - frst three ducks delivered and spawned into duck holes
     * final duck (11) delivered triggers state change, use assemblySO IMAP11->IMAPAssembled
     * Assembling: timer before assembled duck is spawned on pallet -> could add some FX to duckholes
     * Corrupting: As soon as Assembled duck is spawned it starts Corrupting -> corruption timer starts
     * Corrupt state

    */

    public enum State
    {
        Idle,
        Assembling,
        Corrupting,
        Corrupt
    }

    //list to hold corruption SO eg assembledIMAP -> corrupt
    [SerializeField]
    private CorruptionSO[] CorruptionSOArray;

    //List to hold assembly SO eg IMAP11 -> AssembledIMAP
    [SerializeField]
    private AssemblySO[] AssemblySOArray;


    private State state;

    //Timers
    private float assemblyTimer;
    private float corruptionTimer;

    //scriptable objects
    private AssemblySO assemblySO;
    private CorruptionSO corruptionSO;


    private void Awake()
    {
        duckObjectSOList = new List<DucksSO>();
    }


    private void Start()
    {
        //initialise state machine
        state = State.Idle;
    }

    




    private void Update()
    {
        //from duckholes
        duckHoleTimer += Time.deltaTime;
        //Debug.Log(duckHoleTimer);
        if (duckHoleTimer > duckHoleTimerMax)
        {
            duckHoleTimer = 0f;
            ducksSpawned--;
            OnDestroyLast?.Invoke(this, EventArgs.Empty);
            Debug.Log("Reset Timer");
        }

        //assembly state machine
        if (HasDuckObject())
        {
            switch (state)
            {
                case State.Idle:
                    //empty pallet
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    break;

                case State.Assembling:
                    assemblyTimer += Time.deltaTime;
                    

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = assemblyTimer / assemblySO.assemblyProgressMax
                    });

                    if (assemblyTimer > assemblySO.assemblyProgressMax)
                    {//assembling
                        GetDuckObject().DestroySelf();
                        DuckObject.spawnDuckObject(assemblySO.output, this);
                        state = State.Corrupting;
                        corruptionTimer = 0f;
                        corruptionSO = GetCorruptionSOWithInput(GetDuckObject().GetDucksSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;

                case State.Corrupting:
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

                if (HasMatchwithAssemblySOInput(player.GetDuckObject().GetDucksSO()))
                {//duck dropped matches assemblySO.input duck object within pallet's array

                    //when E is pressed the duck is parented to this pallet

                    player.GetDuckObject().SetDuckObjectParent(this);
                    //Debug.Log("This pallet now has a " + GetDuckObject().name);
                    assemblySO = GetAssemblySOWithInput(GetDuckObject().GetDucksSO());
                    state = State.Assembling;
                    assemblyTimer = 0f;


                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = assemblyTimer / assemblySO.assemblyProgressMax
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

    private bool HasMatchwithAssemblySOInput(DucksSO inputDuckSO)
    {
        AssemblySO assemblySO = GetAssemblySOWithInput(inputDuckSO);
        if (assemblySO != null)
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
        AssemblySO assemblySO = GetAssemblySOWithInput(inputDuckSO);
        if (assemblySO != null)
        {
            return assemblySO.output;
        }
        else
        {
            return null;
        }
    }

    private AssemblySO GetAssemblySOWithInput(DucksSO inputDucksSO)
    {
        foreach (AssemblySO assemblySO in AssemblySOArray)
        {
            if (assemblySO.input == inputDucksSO)
            {
                return assemblySO;
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
