using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHolesVisual : MonoBehaviour
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
        float duckOffsetZ = 1.25f;
        float duckOffsetY = 0.95f;
        int colOffset = duckVisualGameObjectList.Count % 6 ;

        if (duckVisualGameObjectList.Count < 6)
        {
            duckVisualTransform.localPosition = new Vector3(0, 0, -duckOffsetZ * duckVisualGameObjectList.Count);
        }
        else if (duckVisualGameObjectList.Count < 12)
        {
            duckVisualTransform.localPosition = new Vector3(0, -duckOffsetY, -duckOffsetZ * colOffset);
        }
        else if (duckVisualGameObjectList.Count < 18)
        {
            duckVisualTransform.localPosition = new Vector3(0, -duckOffsetY * 2, -duckOffsetZ * colOffset);
        }
        else
        {
            duckVisualTransform.localPosition = new Vector3(0, -duckOffsetY * 3, -duckOffsetZ * colOffset);
        }

        duckVisualGameObjectList.Add(duckVisualTransform.gameObject);
    }
}
