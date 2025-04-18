using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssemblyPalletDuckHolesVisual : MonoBehaviour
{
    private List<GameObject> duckVisualGameObjectList;
    private Transform duckToSpawnVisualPrefabTransform;
    private DucksSO duckToSpawn;

    [SerializeField]
    private Transform topLeftSpawnPoint;

    [SerializeField]
    private AssemblyPalletDuckHoles assemblyPalletDuckHoles;

    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private GameObject electricity;

    [SerializeField]
    private GameObject sparks;

    [SerializeField]
    private GameObject electricDucks;


    private void Awake()
    {
        duckVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        assemblyPalletDuckHoles.OnStateChanged += AssemblyPalletDuckHoles_OnStateChanged;
        assemblyPalletDuckHoles.OnDuckSpawned += AssemblyPalletDuckHoles_OnDuckSpawned;
        assemblyPalletDuckHoles.OnDestroyLast += AssemblyPalletDuckHoles_OnDestroyLast;
        assemblyPalletDuckHoles.OnDestroyAll += AssemblyPalletDuckHoles_OnDestroyAll;
        assemblyPalletDuckHoles.OnClearIcons += AssemblyPalletDuckHoles_OnClearIcons;
    }

    private void AssemblyPalletDuckHoles_OnClearIcons(object sender, EventArgs e)
    {
        ClearDuckObjectLists();
    }

    private void AssemblyPalletDuckHoles_OnStateChanged(object sender, AssemblyPalletDuckHoles.OnStateChangedEventArgs e)
    {//update visual fx for relevant state
        bool idleVisuals = e.state == AssemblyPalletDuckHoles.State.Idle;
        bool assemblingVisuals = e.state == AssemblyPalletDuckHoles.State.Assembling;
        bool corruptingVisuals = e.state == AssemblyPalletDuckHoles.State.Corrupting;
        bool corruptVisuals = e.state == AssemblyPalletDuckHoles.State.Corrupt;
        

        if (assemblingVisuals)
        {
            smoke.SetActive(true);
            sparks.SetActive(false);
            electricity.SetActive(false);
            electricDucks.SetActive(true);//ghost duck sparks
        }
        else if (corruptingVisuals)
        {
            smoke.SetActive(true);
            sparks.SetActive(true);
            electricity.SetActive(false);
            electricDucks.SetActive(false);
        }
        else if (corruptVisuals)
        {
            smoke.SetActive(false);
            sparks.SetActive(true);
            electricity.SetActive(true);
            electricDucks.SetActive(false);
        }
        else if (idleVisuals)
        {
            smoke.SetActive(false);
            sparks.SetActive(false);
            electricity.SetActive(false);
            electricDucks.SetActive(false);
        }
    }


    private void AssemblyPalletDuckHoles_OnDestroyAll(object sender, EventArgs e)
    {
        foreach (GameObject duckToDestroy in duckVisualGameObjectList)
        {
            Destroy(duckToDestroy);
            assemblyPalletDuckHoles.duckObjectSOList.Clear();

            //duckToDestroy.SetActive(false);

        }
        //ResetDuckHoleVisuals();
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

    private void ClearDuckObjectLists()
    {
        duckVisualGameObjectList.Clear();
        assemblyPalletDuckHoles.duckObjectSOList.Clear();
    }

    private void ResetDuckHoleVisuals()
    {
        foreach (Transform child in topLeftSpawnPoint.transform)
        {
            Destroy(child.gameObject);
        }
        //{
        //    //don't delete the icon template, just the icons/backgrounds
        //    if (child == iconTemplate) continue;
        //    Destroy(child.gameObject);
        //}
    }
}
