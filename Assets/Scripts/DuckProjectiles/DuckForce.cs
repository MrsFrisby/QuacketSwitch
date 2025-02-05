using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckForce : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField]
    float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);

        //rb.velocity = transform.forward * force;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(transform.forward * force, ForceMode.Force);
    }
}
