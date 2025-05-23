using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public event EventHandler OnProtocolSpawned;
    public event EventHandler OnProtocolCompleted;
    public event EventHandler OnProtocolSuccess;
    public event EventHandler OnProtocolFailed;
    public event EventHandler OnProtocolDelivered;

    public static AssemblyManager Instance { get; private set; }

    [SerializeField] private ProtocolListSO protocolListSO;
    [SerializeField] private int waitingProtocolsMax;

    private List<ProtocolSO> waitingProtocolSOList;

    private float spawnProtocolTimer;
    private float spawnProtocolTimerMax = 5f;
    //private int waitingProtocolsMax = 4;
    private int successfulTransmissionsDelivered;

    private void Awake()
    {
        //Debug.Log("Assembly Manager awake");
        Instance = this;
        waitingProtocolSOList = new List<ProtocolSO>();
    }

    private void Update()
    {
        spawnProtocolTimer -= Time.deltaTime;
        //Debug.Log("AM:"+spawnProtocolTimer);
        if (spawnProtocolTimer <= 0f)
        {
            spawnProtocolTimer = spawnProtocolTimerMax;


            if (GameManager.Instance.IsGamePlaying() && waitingProtocolSOList.Count < waitingProtocolsMax)
            {
                ProtocolSO waitingProtocolSO = protocolListSO.protocolSOList[UnityEngine.Random.Range(0, protocolListSO.protocolSOList.Count)];
                //Debug.Log("Assembly Manager:"+waitingProtocolSO.name);
                waitingProtocolSOList.Add(waitingProtocolSO);

                OnProtocolSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public void DeliverAssembledProtocol(AssembledDuckObject assembledDuckObject)
    {
        OnProtocolDelivered?.Invoke(this, EventArgs.Empty);
        for (int i=0; i<waitingProtocolSOList.Count; i++)
        {
            ProtocolSO waitingProtocolSO = waitingProtocolSOList[i];
            //Debug.Log("assemblyManager:waitingprotocolSO:" + waitingProtocolSO);

            if(waitingProtocolSO.duckObjectSOList.Count == assembledDuckObject.GetDucksSOList().Count)
            {//delivered assembled duck has same number of duck components as waiting protocol
                //Debug.Log("Match between number of ducks in waiting protocol and delivered assembled duck");
                bool assembledDuckMatchesProtocol = true;
                //this bool is set to false if a match can't be found 
                foreach (DucksSO waitingProtocol_DucksSO in waitingProtocolSO.duckObjectSOList)
                {//loop through each duck in the waiting protocol
                    bool duckFound = false;
                    foreach(DucksSO assembledDuck_DucksSO in assembledDuckObject.GetDucksSOList())
                    {//nested loop through each duck so in the delivered assembled duck
                        if (waitingProtocol_DucksSO == assembledDuck_DucksSO)
                        {
                            //Debug.Log(waitingProtocol_DucksSO + " matches " + assembledDuck_DucksSO);
                            duckFound = true;
                            break;//stop inner loop, move to next duck in waiting protocol outer loop
                        }
                    }//break exits inner foreach loop
                    if(!duckFound)
                    {//a matching couldn't be made with at least one duck in the waiting protocol
                        assembledDuckMatchesProtocol = false;
                        //Debug.Log("Reached End of duck matching loop: no match found");
                    }
                }
                if (assembledDuckMatchesProtocol)
                {//everything matches!
                    //Debug.Log("Correct Assembly Duck Delivered");
                    //remove the completed protocol from the list
                    waitingProtocolSOList.RemoveAt(i);
                    OnProtocolCompleted?.Invoke(this, EventArgs.Empty);
                    OnProtocolSuccess?.Invoke(this, EventArgs.Empty);
                    successfulTransmissionsDelivered++;
                    return;
                }
            }
            else
            {//else no match between count 
                //Debug.Log("Count match failed");
            }  
        }
        // End of for loop - if we reach here there were No matches
        //Debug.Log("Reached End of for loop: Correct assembly not delivered");
        OnProtocolFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<ProtocolSO> GetWaitingProtocolSOList()
    {
        //Debug.Log("AM:GetWaitingProtocolSOList");
        return waitingProtocolSOList;
    }

    public int GetSuccessfulTransmissions()
    {
        return successfulTransmissionsDelivered;
    }
}
