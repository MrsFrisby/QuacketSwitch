using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyPalletDuckHoles : BasePallet, IHasProgress

{
    public static event EventHandler OnAnyIdle;
    public static event EventHandler OnAnyAssembling;
    public static event EventHandler OnAnyCorrupting;
    public static event EventHandler OnAnyCorrupt;
    public event EventHandler OnClearIcons;
    public event EventHandler OnDestroyAll;
    public event EventHandler OnDuckSpawned;
    public event EventHandler OnDestroyLast;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }


    private float duckHoleTimer;
    private float duckHoleTimerMax = 320f;
    private int ducksSpawned;
    private int ducksSpawnedMax = 4;

    public List<DucksSO> duckObjectSOList;

    [SerializeField] private DucksSO ghostDuck;

    [SerializeField] private List<DucksSO> validDucksSOList;

    private DucksSO playerDuckSO;

    //private DucksSO playerFinalDuckSO;

    

    

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


    private State currentState;

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
        currentState = State.Idle;
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
            switch (currentState)
            {
                case State.Idle:

                    //play idle audioFX - electrical hum/buzz
                    OnAnyIdle?.Invoke(this, EventArgs.Empty);

                    //empty pallet
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = currentState
                    });
                    break;

                case State.Assembling:

                    assemblyTimer += Time.deltaTime;
                    //Debug.Log("Timer: " + assemblyTimer);

                    //remove duck prefabs from duckholes
                    OnDestroyAll?.Invoke(this, EventArgs.Empty);

                    OnAnyAssembling?.Invoke(this, EventArgs.Empty);

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = assemblyTimer / assemblySO.assemblyProgressMax
                    });

                    
                    if (assemblyTimer > assemblySO.assemblyProgressMax)
                    {//assembling

                        //there is no duck on the pallet
                        GetDuckObject().DestroySelf();
                        DuckObject.spawnDuckObject(assemblySO.output, this);
                        currentState = State.Corrupting;
                        corruptionTimer = 0f;
                        corruptionSO = GetCorruptionSOWithInput(GetDuckObject().GetDucksSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = currentState
                        });

                        
                    }
                    break;

                case State.Corrupting:
                    corruptionTimer += Time.deltaTime;
                    //Debug.Log("Corrupting state)");
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = corruptionTimer / corruptionSO.corruptionTimerMax
                    });
                    //remove UI icons
                    OnClearIcons?.Invoke(this, EventArgs.Empty);

                    //play corrupting audioFX
                    OnAnyCorrupting?.Invoke(this, EventArgs.Empty);

                    if (corruptionTimer > corruptionSO.corruptionTimerMax)
                    {//corrupt
                        GetDuckObject().DestroySelf();
                        DuckObject.spawnDuckObject(corruptionSO.output, this);
                        currentState = State.Corrupt;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = currentState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Corrupt:

                    //play corrupt audioFX
                    OnAnyCorrupt?.Invoke(this, EventArgs.Empty);
                    break;

            }
        }
    }

    public override void Interact(Player player)
    {
        //Debug.Log("CookerPallet Interact)");
        if (player.HasDuckObject())//when E is pressed
        {
            playerDuckSO = player.GetDuckObject().GetDucksSO();
            if (HasMatchWithValidDuckSOList(playerDuckSO))
            {
                if (TryAddDucktoList(playerDuckSO))
                {// returns true if the duck is not already on the list
                    player.GetDuckObject().DestroySelf();

                    //check there is space for more ducks
                    if (ducksSpawned < ducksSpawnedMax)
                    {
                        ducksSpawned++;
                        OnDuckSpawned?.Invoke(this, EventArgs.Empty);

                        //check if player is delivering final duck
                        if (ducksSpawned == ducksSpawnedMax)
                        {
                            //check that we have an assemblySO for final duck delivered
                            if (HasMatchwithAssemblySOInput(playerDuckSO))
                            {//duck dropped matches assemblySO.input duck object within pallet's array

                                //spawn ghost ducks as child duckSO of this pallet
                                DuckObject.spawnDuckObject(ghostDuck,this);
                                assemblySO = GetAssemblySOWithInput(playerDuckSO);
                                currentState = State.Assembling;

                                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                                {
                                    state = currentState
                                });

                                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                                {
                                    progressNormalized = assemblyTimer / assemblySO.assemblyProgressMax
                                });
                            }
                            else
                            {
                                Debug.Log("No match with AssemblySOInput: this is a " + playerDuckSO);
                            }
                        }
                    }
                }
            }
            else
            {//No match with Vald duckSO list assigned in inspector
                Debug.Log("Wrong Duck SO for this pallet");
            }
        }

        else
        { //if player is empty handed give duck to player
            Debug.Log("Interact:player should be able to pick up corrupt duck");
            GetDuckObject().SetDuckObjectParent(player);
            currentState = State.Idle;

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                state = currentState
            });

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = 0f
            });
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
