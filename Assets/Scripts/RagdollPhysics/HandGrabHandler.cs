using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabHandler : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    FixedJoint fixedJoint;

    Rigidbody rigidbody3D;

    Player player;

   

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
        rigidbody3D = GetComponent<Rigidbody>();

        //change solver iterations to keep picked up item close to hand
        rigidbody3D.solverIterations = 255;
    }

    public void UpdateState()
    {
        //check player is trying to grab - G key pressed
        if (player.IsGrabbingActive)
        {
            //play grabbing animation - make player bend over to grab duck
            animator.SetBool("isGrabbing", true);
        }
        else
        {
            //if we are already carrying something and G key is released
            if (fixedJoint != null)
            {
                //apply throwing force to connected object on release
                if(fixedJoint.connectedBody != null)
                {
                    float forceAmountMultiplier = 0.8f;

                    fixedJoint.connectedBody.AddForce((player.transform.forward + Vector3.up * 0.25f) * forceAmountMultiplier, ForceMode.Impulse);
                }

                //destroy configurable joint
                Destroy(fixedJoint);
                Debug.Log("fixed joint destroyed");

                //player.grabbedDuck.SetDuckObjectParent(null);
                //Debug.Log("Dropped duck parent: " + player.grabbedDuck.GetDuckObjectParent());
                //player.grabbedDuck = null;

                //if(!player.TryGetComponent(out DuckObject carriedDuckObject))
                //{
                //    Debug.Log("failed to get carriedDuckObject");
                //}
                //else
                //{
                //    carriedDuckObject.SetDuckObjectParent(null);
                //    Debug.Log("Dropped duck parent: "+carriedDuckObject.GetDuckObjectParent());
                //}



            }

            //change animation state to not carrying and not grabbing
            animator.SetBool("isCarrying", false);
            animator.SetBool("isGrabbing", false);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        //when player rb collides with another rb
        TryCarryObject(collision);

    }

    bool TryCarryObject(Collision collision)
    {
        //check if ragdoll is not active - don't accidentally pick up ducks when in ragdoll stae
        if (!player.IsActiveRagdoll)
            return false;
        //check if player is not trying to grab
        if (!player.IsGrabbingActive)
            return false;
        //check if player is not carrying duck
        if (fixedJoint != null)
            return false;
        //don't grab self
        if (collision.transform.root == player.transform)
            return false;
        //check item has rigid body - so player can't pick up scenery/pallets etc
        if (!collision.collider.TryGetComponent(out Rigidbody otherObjectRigidbody))
            return false;

        //if we get this far, player can pick item up
        fixedJoint = transform.gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = otherObjectRigidbody;

        fixedJoint.autoConfigureConnectedAnchor = false;


        //this is where we need to update duck parent to player

        //if(!collision.collider.TryGetComponent(out DuckObject collisionDuckObject))
        //{
        //    Debug.Log("couldn't get duckObject, hit a "+ collision.gameObject.name);

        //}
        //else
        //{
        //    collisionDuckObject.SetDuckObjectParent(player);
        //    player.grabbedDuck = collisionDuckObject;
        //    Debug.Log("Grabbed duck parent: " + collisionDuckObject.GetDuckObjectParent());
        //}
        
        




        //transform the collision point from world to local space
        fixedJoint.connectedAnchor = collision.transform.InverseTransformPoint(collision.GetContact(0).point);

        animator.SetBool("isCarrying", true);

        return true;

    }


    

    
}
