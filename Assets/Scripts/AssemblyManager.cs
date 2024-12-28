using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public static AssemblyManager Instance { get; private set; }

    [SerializeField] private ProtocolListSO protocolListSO; 

    private List<ProtocolSO> waitingProtocolSOList;

    private float spawnProtocolTimer;
    private float spawnProtocolTimerMax = 5f;
    private int waitingProtocolsMax = 10;

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
                ProtocolSO waitingProtocolSO = protocolListSO.protocolSOList[Random.Range(0, protocolListSO.protocolSOList.Count)];
                Debug.Log(waitingProtocolSO.name);
                waitingProtocolSOList.Add(waitingProtocolSO);
            }
            
        }
    }

    public void DeliverAssembledProtocol(AssembledDuckObject assembledDuckObject)
    {
        bool assembledDuckMatchesProtocol = false;


        for (int i=0; i<waitingProtocolSOList.Count; i++)
        {
            ProtocolSO waitingProtocolSO = waitingProtocolSOList[i];

            if (waitingProtocolSO == assembledDuckObject)
            {
                assembledDuckMatchesProtocol = true;
            }
            //else
            //{
            //    assembledDuckMatchesProtocol = false;
            //}

            if (assembledDuckMatchesProtocol)
            {
                Debug.Log("Correct Assembly Duck Delivered");
                waitingProtocolSOList.RemoveAt(i);
                return;

            }
        }
        //No matches
        Debug.Log("Correct assembly not delivered");
        
    }
}
