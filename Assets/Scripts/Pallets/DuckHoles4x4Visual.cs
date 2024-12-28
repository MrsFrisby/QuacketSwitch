using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHoles4x4Visual : MonoBehaviour
{
    [SerializeField] private DuckHoles duckHoles;
    [SerializeField] private Transform topLeftSpawnPoint;
    [SerializeField] private Transform duckVisualPrefab;

    private List<GameObject> duckVisualGameObjectList;


    private void Awake()
    {
        duckVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        duckHoles.OnDuckSpawned += DuckHoles_OnDuckSpawned;
        duckHoles.OnDestroyLast += DuckHoles_OnDestroyLast;
    }

    private void DuckHoles_OnDestroyLast(object sender, EventArgs e)
    {
        //Debug.Log("destroy last!");
        if (duckVisualGameObjectList.Count > 1)
        {
            GameObject duckToDestroy = duckVisualGameObjectList[duckVisualGameObjectList.Count - 1];
            duckVisualGameObjectList.Remove(duckToDestroy);
            Destroy(duckToDestroy);
        }
    }


    private void DuckHoles_OnDuckSpawned(object sender, EventArgs e)
    {
        Debug.Log("DuckHoleVisual:trying to spawn");
        Transform duckVisualTransform = Instantiate(duckVisualPrefab, topLeftSpawnPoint);
        float duckOffsetZ = 0.9f;
        float duckOffsetY = 0.9f;
        int colOffset = duckVisualGameObjectList.Count % 2 ;

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
