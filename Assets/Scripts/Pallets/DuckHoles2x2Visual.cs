using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DuckHoles2x2Visual : MonoBehaviour
{
    [SerializeField] private DuckHoles2x2 duckHoles;
    [SerializeField] private Transform topLeftSpawnPoint;
    [SerializeField] private Transform duckVisualPrefab;

    //[Serializable]
    //public struct duckSO_GameObject
    //{ //struct used instead of class to store data without logic
    //    public DucksSO ducksSO;
    //    public GameObject duckGameObject;
    //}

    //[SerializeField]
    //private List<duckSO_GameObject> duckSO_GameObjectList;

    private List<GameObject> duckVisualGameObjectList;

    //private DuckObject duckToSpawnDuckObject;
    private Transform duckToSpawnVisualPrefabTransform;
    private DucksSO duckToSpawn;


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

    //position spawned ducks correctly starting at top left spawn point
    private void DuckHoles_OnDuckSpawned(object sender, EventArgs e)
    {

        duckToSpawn = duckHoles.duckObjectSOList.Last();
        duckToSpawnVisualPrefabTransform = duckToSpawn.visualPrefab;
        //duckToSpawnDuckObject = duckToSpawn.duckObject;

        //duckToSpawnDuckObject.SetIconVisibilityInActive();
        Debug.Log("DuckHoleVisual:trying to spawn: "+ duckToSpawnVisualPrefabTransform);
        //Transform duckVisualTransform = Instantiate(duckVisualPrefab, topLeftSpawnPoint);
        Transform duckVisualTransform = Instantiate(duckToSpawnVisualPrefabTransform, topLeftSpawnPoint);
        //duckToSpawnGameObject = duckVisualTransform.gameObject;
        //duckToSpawnGameObject.SetIconVisibilityInActive();
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
