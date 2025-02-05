using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFire : MonoBehaviour
{

    [SerializeField]
    float duckSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * duckSpeed);
        transform.Translate(transform.forward * Time.deltaTime * duckSpeed);
    }
}
