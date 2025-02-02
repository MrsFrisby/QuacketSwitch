using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPoolReturn : MonoBehaviour
{

    [SerializeField]
    private float timeToLiveMax;

    [SerializeField]
    private float timeToLiveMin;

    private float timeToLive;

    private float timeSinceSpawn;

    private DuckPool duckPool;

    void Start()
    {
        duckPool = FindObjectOfType<DuckPool>();
        timeToLive = Random.Range(timeToLiveMin, timeToLiveMax);
    }

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= timeToLive)
        {
            if (duckPool != null)
            {
                duckPool.ReturnDuckProjectile(this.gameObject);
            }
        }
    }

    //private void OnDisable()
    //{
    //    if(duckPool != null)
    //    {
    //        duckPool.ReturnDuckProjectile(this.gameObject);
    //    }
    //}
}
