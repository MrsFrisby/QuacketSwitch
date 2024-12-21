using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{

    Player player;

    Rigidbody hitRigidbody;

    ContactPoint[] contactPoints = new ContactPoint[5];

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        hitRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!player.IsActiveRagdoll)
            return;

        if (!collision.collider.CompareTag("CauseDamage"))
            return;

        //don't get knocked out while carrying ducks
        if (collision.collider.transform.root == player.transform)
            return;

        int numberOfContacts = collision.GetContacts(contactPoints);

        for (int i = 0; i < numberOfContacts; i++)
        {
            ContactPoint contactPoint = contactPoints[i];

            //get contact impulse force
            Vector3 contactImpulse = contactPoint.impulse / Time.fixedDeltaTime;

            //check if force was significant enough to cause knock out
            if (contactImpulse.magnitude < 15)
                continue; //check next i

            player.OnBodyPartHit();

            Vector3 forceDirection = (contactImpulse + Vector3.up) * 0.5f;

            //limit the force so player isn't thrown too far
            forceDirection = Vector3.ClampMagnitude(forceDirection, 30);

            Debug.DrawRay(hitRigidbody.position, forceDirection * 40, Color.red, 4);

            //increase the effect of impact hit
            hitRigidbody.AddForce(forceDirection, ForceMode.Impulse);

        }
           
    }
}
