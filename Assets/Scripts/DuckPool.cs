using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class DuckPool : MonoBehaviour
{
    [SerializeField]
    private GameObject corruptDuck;

    [SerializeField]
    private Queue<GameObject> duckPool = new Queue<GameObject>();

    [SerializeField]
    private int poolStartSize = 20;

    private void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject duckProjectile = Instantiate(corruptDuck);
            duckPool.Enqueue(duckProjectile);
            duckProjectile.SetActive(false);
        }
    }

    public GameObject GetDuckProjectile()
    {
        if (duckPool.Count > 0)
        {
            GameObject duckProjectile = duckPool.Dequeue();
            duckProjectile.SetActive(true);
            return duckProjectile;
        }
        else
        {
            GameObject duckProjectile = Instantiate(corruptDuck);
            return duckProjectile;
        }
    }

    public void ReturnDuckProjectile(GameObject duckProjectile)
    {
        duckPool.Enqueue(duckProjectile);
        duckProjectile.SetActive(false);
    }
}
