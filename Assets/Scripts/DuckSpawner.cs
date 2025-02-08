using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DuckSpawner : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 5f;

    
    private float timeSinceSpawn;

    [SerializeField]
    private DuckPool duckPool;

    
    void Start()
    {
        //duckPool = FindObjectOfType<DuckPool>();
    }

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn>= timeToSpawn)
        {
            GameObject newDuckProjectile = duckPool.GetDuckProjectile();
            newDuckProjectile.transform.position = this.transform.position;
            timeSinceSpawn = 0f;
        }
    }

}
