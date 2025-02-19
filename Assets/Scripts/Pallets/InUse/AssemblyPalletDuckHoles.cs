using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//inherits from basePallet class and HasProgress interface
public class AssemblyPalletDuckHoles : BasePallet, IHasProgress

{
    //public static AssemblyPalletDuckHoles Instance { get; private set; }



    //static events that can be called from any instance of an AssemblyPalletDuckHoles
    public static event EventHandler OnAnyIdle;
    public static event EventHandler OnAnyAssembling;
    public static event EventHandler OnAnyCorrupting;
    public static event EventHandler OnAnyCorrupt;
    

    //reset static data to prevent errors on restart
    new public static void ResetStaticData()
    {
        OnAnyIdle = null;
        OnAnyAssembling = null;
        OnAnyCorrupting = null;
        OnAnyCorrupt = null;
    }

    public event EventHandler OnClearIcons;
    public event EventHandler OnDestroyAll;
    public event EventHandler OnDuckSpawned;
    public event EventHandler OnDestroyLast;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    //public static event EventHandler<OnDuckDeliveredEventArgs> OnDuckDelivered;

    //public class OnDuckDeliveredEventArgs : EventArgs
    //{
    //    public Container containerToDeactivate;
    //}

    public static event EventHandler<OnDuckDeliveredEventArgs> OnDuckDelivered;

    public class OnDuckDeliveredEventArgs : EventArgs
    {
        public DucksSO containerDuckSO;
    }

    public static event EventHandler onDuckAssembled;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    //collected ducks disappear if left too long
    private float duckHoleTimer;
    private float duckHoleTimerMax = 320f;//time after which last duck is deleted
    private int ducksSpawned;//counter
    private int ducksSpawnedMax = 4;

    public List<DucksSO> duckObjectSOList;

    public List<DuckObject> duckObjectsList;

    //private int duckCount;

    [SerializeField] private DucksSO ghostDuck;

    [SerializeField] private List<DucksSO> validDucksSOList;

    private DucksSO playerDuckSO;

    
    /*states
     * Idle: empty duck holes/idle - frst three ducks delivered and spawned into duck holes
     * final duck delivered triggers state change, use assemblySO eg IMAP11->IMAPAssembled
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
    //as ducks can be added in any order
    [SerializeField]
    private AssemblySO[] AssemblySOArray;

    //[SerializeField]
    //private DuckObject[] duckObjectsArray;

    [SerializeField]
    private DeactivatePalletSO[] DeactivatePalletSOArray;

    private State currentState;

    //Timers
    private float assemblyTimer;
    private float corruptionTimer;

    //scriptable objects
    private AssemblySO assemblySO;
    private CorruptionSO corruptionSO;
    

    private void Awake()
    {
        //if (Instance != null)
        //{
        //    Debug.LogError("Instance is not null");
        //}
        //Instance = this;

        //create a new list to keep track of ducks as the player adds them to the duck holes
        duckObjectSOList = new List<DucksSO>();
        duckObjectsList = new List<DuckObject>();
    }


    private void Start()
    {
        //initialise state machine
        currentState = State.Idle;     
    }

    private void Update()
    {
        //destroy last added duck if timer reaches max
        duckHoleTimer += Time.deltaTime;
        if (duckHoleTimer > duckHoleTimerMax)
        {
            duckHoleTimer = 0f;//reset timer
            ducksSpawned--;//decrement duck counter
            OnDestroyLast?.Invoke(this, EventArgs.Empty);//destroy last duck added
        }

        //assembly state machine
        if (HasDuckObject())
        {//if the last duck has been delivered
            switch (currentState)
            {
                case State.Idle:
                    assemblyTimer = 0f;
                    //play idle audioFX - electrical hum/buzz
                    OnAnyIdle?.Invoke(this, EventArgs.Empty);

                    //update state
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = currentState
                    });
                    break;

                case State.Assembling://ghost ducks visible

                    assemblyTimer += Time.deltaTime;//start assembly timer

                    //reactivate pallets and remove duck objects from list
                    foreach (DuckObject duckObject in duckObjectsList)
                    {
                        //duckObject.ReactivatePallet();
                        duckObjectsList.Remove(duckObject);
                    }

                    //remove duck prefabs from duckholes and duckSOs from list
                    OnDestroyAll?.Invoke(this, EventArgs.Empty);
                    ducksSpawned = 0;

                    //start assembling audio
                    OnAnyAssembling?.Invoke(this, EventArgs.Empty);

                    //update assembly progress bar
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = assemblyTimer / assemblySO.assemblyProgressMax
                    });
 
                    if (assemblyTimer > assemblySO.assemblyProgressMax)
                    {//change from ghost ducks to assembled duck

                        //destroy the ghost ducks
                        GetDuckObject().DestroySelf();
                        //spawn the correct assembled duck for the input protocol
                        DuckObject.spawnDuckObject(assemblySO.output, this);
                        currentState = State.Corrupting;
                        corruptionTimer = 0f;//initialise corruption timer
                        corruptionSO = GetCorruptionSOWithInput(GetDuckObject().GetDucksSO());
                        //all assembled duckSO list corrupt duck

                        //update state to corrupting
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = currentState
                        });
                    }
                    break;

                case State.Corrupting:
                    corruptionTimer += Time.deltaTime;//start corruption timer

                    //update progress bar
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = corruptionTimer / corruptionSO.corruptionTimerMax
                    });
                    //remove UI icons
                    OnClearIcons?.Invoke(this, EventArgs.Empty);

                    //play corrupting audioFX
                    OnAnyCorrupting?.Invoke(this, EventArgs.Empty);

                    //notify all protocol container pallets to reactivate 
                    onDuckAssembled?.Invoke(this, EventArgs.Empty);

                    if (corruptionTimer > corruptionSO.corruptionTimerMax)
                    {
                        //destroy the assembly duck
                        GetDuckObject().DestroySelf();
                        //spawn the corrupt duck
                        DuckObject.spawnDuckObject(corruptionSO.output, this);
                        currentState = State.Corrupt;

                        //update state
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = currentState
                        });

                        //reset corruption timer progress bar
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
    {//when E is pressed
        //Debug.Log("DuckHoles1: E pressed");
        if (player.HasDuckObject())
        {//find out which duck the player is carrying
            //Debug.Log("DuckHoles2: player has duck");
            playerDuckSO = player.GetDuckObject().GetDucksSO();
            if (HasMatchWithValidDuckSOList(playerDuckSO))//if the duck can be accepted
            {// returns true if the duck is not already on the list
                //Debug.Log("DuckHoles3: duck match true");
                if (TryAddDucktoList(playerDuckSO))
                {//get the duckObject that has been delivered
                    //cycle through the deactivatePalletSOArray to find the matching pallet 
                    //Container container = GetContainerWithDuckInput(playerDuckSO);
                    Debug.Log("PlayerDuckSO:" + playerDuckSO);
                    //Debug.Log("APDH Interact:" + container);
                    //string playerDuckSONameString = playerDuckSO.name.ToString();
                    //publish event to container pallets to deactivate
                    //if (playerDuckSONameString == "FTP00")
                    //{
                    //    OnFTP00DuckDelivered?.Invoke(this, EventArgs.Empty);
                    //}
                    //else if (playerDuckSONameString == "FTP01")
                    //{
                    //    OnFTP01DuckDelivered?.Invoke(this, EventArgs.Empty);
                    //}
                    //else if (playerDuckSONameString == "FTP10")
                    //{
                    //    OnFTP10DuckDelivered?.Invoke(this, EventArgs.Empty);
                    //}

                    OnDuckDelivered?.Invoke(this, new OnDuckDeliveredEventArgs
                    {
                        containerDuckSO = playerDuckSO
                    }); ;





                    //OnDuckDelivered?.Invoke(this, new OnDuckDeliveredEventArgs
                    //{
                    //    containerToDeactivate = container
                    //}); 
                    //Debug.Log("DuckHoles4: duck added to pallet SO list");
                    //destroy the duck the player is holding
                    player.GetDuckObject().DestroySelf();
                    if (ducksSpawned < ducksSpawnedMax)
                    {//there is room for more ducks
                        //Debug.Log("DuckHoles5: room for more ducks");
                        ducksSpawned++;//increment counter
                        OnDuckSpawned?.Invoke(this, EventArgs.Empty);//broadcast to visual script
                        if (ducksSpawned == ducksSpawnedMax)
                        {//if player is delivering final duck
                            //Debug.Log("DuckHoles6: player has last duck");
                            if (HasMatchwithAssemblySOInput(playerDuckSO))
                            {//duck dropped matches assemblySO.input duck object within pallet's array
                                //spawn ghost ducks as child duckSO of this pallet
                                //Debug.Log("DuckHoles7: match found with Assembly SO input");
                                DuckObject.spawnDuckObject(ghostDuck,this);
                                //get the correct output assembled duck based on the last duck successfully delivered
                                assemblySO = GetAssemblySOWithInput(playerDuckSO);
                                currentState = State.Assembling;
                                assemblyTimer = 0f;
                                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                                {//update state from idle to assembling
                                    state = currentState
                                });
                                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                                {//initialize progress bar for assembly timer
                                    progressNormalized = assemblyTimer / assemblySO.assemblyProgressMax
                                });
                            }
                            else
                            {//this shouldn't ever happen
                                //Debug.Log("Duckholes7: No match with AssemblySOInput: this is a " + playerDuckSO);
                            }
                        }
                        else
                        {
                            //Debug.Log("DuckHoles6: not the last duck");
                        }
                    }
                    else
                    { //Debug.Log("DuckHoles5: no room for more ducks");
                    }
                }
                else { //Debug.Log("DuckHoles4: duck not added to SO list");
                     }
            }
            else
            {//No match with Valid duckSO list assigned in inspector
                DucksSO deliveredDuck = player.GetDuckObject().GetDucksSO();
                Debug.Log("Duckholes3: Wrong Duck SO for this pallet:"+ deliveredDuck);
            }
        }

        else
        { //if player is empty handed give duck to player
            Debug.Log("DuckHoles2: Player not carrying a duck");
            GetDuckObject().SetDuckObjectParent(player);//reparent duck on pallet to player
            currentState = State.Idle;

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {//reset state back to idle
                state = currentState
            });

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {//reset progress bar (bar hidden when set to 0)
                progressNormalized = 0f
            });
        } 
    }

    //Helper Functions

    //returns a bool - check for a match- proceed only when true
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

    private Container GetContainerWithDuckInput(DucksSO inputDuckSO)
    {
        foreach (DeactivatePalletSO deactivatepalletSO in DeactivatePalletSOArray)
        {
            Debug.Log("checking:" + inputDuckSO);
            Debug.Log("against:"+deactivatepalletSO.inputDuckObjectSO);
            if (deactivatepalletSO.inputDuckObjectSO == inputDuckSO)
            {
                return deactivatepalletSO.outputPallet;
            }
        }
        Debug.Log("APDH:GetContainerwithDuckInput: returning null");
        return null;
    }

    private bool HasMatchWithValidDuckSOList(DucksSO playerDuckSO)
    {
        if (validDucksSOList.Contains(playerDuckSO))
        {
            return true;
        }
        else
        {   foreach (DucksSO ducksSO in validDucksSOList)
            {
                Debug.Log(ducksSO.name);
            }
            
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

    public bool IsAssembled()
    {
        return currentState == State.Corrupting;
    }


}
