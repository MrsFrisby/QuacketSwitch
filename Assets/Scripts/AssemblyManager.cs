using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public event EventHandler OnProtocolSpawned;
    public event EventHandler OnProtocolCompleted;



    public static AssemblyManager Instance { get; private set; }

    [SerializeField] private ProtocolListSO protocolListSO; 



    private List<ProtocolSO> waitingProtocolSOList;

    private float spawnProtocolTimer;
    private float spawnProtocolTimerMax = 5f;
    private int waitingProtocolsMax = 5;

    private void Awake()
    {
        Instance = this;
        waitingProtocolSOList = new List<ProtocolSO>();
    }


    private void Update()
    {
        spawnProtocolTimer -= Time.deltaTime;
        if (spawnProtocolTimer <= 0f)
        {
            spawnProtocolTimer = spawnProtocolTimerMax;


            if (waitingProtocolSOList.Count < waitingProtocolsMax)
            {
                ProtocolSO waitingProtocolSO = protocolListSO.protocolSOList[UnityEngine.Random.Range(0, protocolListSO.protocolSOList.Count)];
                //Debug.Log(waitingProtocolSO.name);
                waitingProtocolSOList.Add(waitingProtocolSO);

                OnProtocolSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public void DeliverAssembledProtocol(AssembledDuckObject assembledDuckObject)
    {
        for (int i=0; i<waitingProtocolSOList.Count; i++)
        {
            ProtocolSO waitingProtocolSO = waitingProtocolSOList[i];

            if(waitingProtocolSO.duckObjectSOList.Count == assembledDuckObject.GetDucksSOList().Count)
            {//delivered assembled duck has same number of duck components as waiting protocol
                bool assembledDuckMatchesProtocol = false;
                foreach (DucksSO protocolDucksSO in waitingProtocolSO.duckObjectSOList)
                {
                    bool duckFound = false;
                    foreach(DucksSO assembledDucksSO in assembledDuckObject.GetDucksSOList())
                    {
                        if (waitingProtocolSO == assembledDuckObject)
                        {
                            duckFound = true;
                            break;//stop looping
                        }
                    }
                    if(!duckFound)
                    {
                        assembledDuckMatchesProtocol = false;
                    }
                }
                if (assembledDuckMatchesProtocol)
                {
                    Debug.Log("Correct Assembly Duck Delivered");
                    waitingProtocolSOList.RemoveAt(i);
                    OnProtocolCompleted?.Invoke(this, EventArgs.Empty);
                    return;

                }
            }    
        }
        //No matches
        Debug.Log("Correct assembly not delivered");
    }

    public List<ProtocolSO> GetWaitingProtocolSOList()
    {
        return waitingProtocolSOList;
    }
}
