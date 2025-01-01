using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssemblyPalletDuckHolesVisual : MonoBehaviour
{
    //from duck holes visual
    //[SerializeField] private DuckHoles2x2 duckHoles;
    [SerializeField] private Transform topLeftSpawnPoint;
    //[SerializeField] private Transform duckVisualPrefab;

    private List<GameObject> duckVisualGameObjectList;
    private Transform duckToSpawnVisualPrefabTransform;
    private DucksSO duckToSpawn;


    //from cooker pallet
    [SerializeField]
    private AssemblyPalletDuckHoles assemblyPalletDuckHoles;

    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private GameObject electricity;

    [SerializeField]
    private GameObject sparks;


    private void Awake()
    {
        duckVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        assemblyPalletDuckHoles.OnStateChanged += AssemblyPalletDuckHoles_OnStateChanged;
        assemblyPalletDuckHoles.OnDuckSpawned += AssemblyPalletDuckHoles_OnDuckSpawned;
        assemblyPalletDuckHoles.OnDestroyLast += AssemblyPalletDuckHoles_OnDestroyLast;
    }

    private void AssemblyPalletDuckHoles_OnStateChanged(object sender, AssemblyPalletDuckHoles.OnStateChangedEventArgs e)
    {
        
        bool showSmokeVisual = e.state == AssemblyPalletDuckHoles.State.Assembling;
        bool showSparksVisual = e.state == AssemblyPalletDuckHoles.State.Corrupting;
        bool showElectricVisual = e.state == AssemblyPalletDuckHoles.State.Corrupt;
        

        if (showSmokeVisual)
        {
            smoke.SetActive(true);
            sparks.SetActive(false);
            electricity.SetActive(false);
        }
        else if (showSparksVisual)
        {
            smoke.SetActive(true);
            sparks.SetActive(true);
            electricity.SetActive(false);
        }
        else if (showElectricVisual)
        {
            smoke.SetActive(false);
            sparks.SetActive(true);
            electricity.SetActive(true);
        }
    }

    private void AssemblyPalletDuckHoles_OnDestroyLast(object sender, EventArgs e)
    {
        //Debug.Log("destroy last!");
        if (duckVisualGameObjectList.Count > 1)
        {
            GameObject duckToDestroy = duckVisualGameObjectList[duckVisualGameObjectList.Count - 1];
            duckVisualGameObjectList.Remove(duckToDestroy);
            Destroy(duckToDestroy);
        }
    }

    //position spawned ducks correctly starting at top left spawn point
    private void AssemblyPalletDuckHoles_OnDuckSpawned(object sender, EventArgs e)
    {
        duckToSpawn = assemblyPalletDuckHoles.duckObjectSOList.Last();
        duckToSpawnVisualPrefabTransform = duckToSpawn.visualPrefab;
        Transform duckVisualTransform = Instantiate(duckToSpawnVisualPrefabTransform, topLeftSpawnPoint);
        float duckOffsetZ = 0.9f;
        float duckOffsetY = 0.9f;
        int colOffset = duckVisualGameObjectList.Count % 2;

        if (duckVisualGameObjectList.Count < 2)
        {
            duckVisualTransform.localPosition = new Vector3(0, 0, -duckOffsetZ * duckVisualGameObjectList.Count);
        }
        else
        {
            duckVisualTransform.localPosition = new Vector3(0, -duckOffsetY, -duckOffsetZ * colOffset);
        }
        duckVisualGameObjectList.Add(duckVisualTransform.gameObject);
    }
}
