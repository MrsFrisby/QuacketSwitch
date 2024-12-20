using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabHandler : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    FixedJoint fixedJoint;

    Rigidbody rigidbody3D;

    CrashTestPlayerController crashTest;

    private void Awake()
    {
        crashTest = transform.root.GetComponent<CrashTestPlayerController>();
        rigidbody3D = GetComponent<Rigidbody>();

        //change solver iterations to keep picked up item close to hand
        rigidbody3D.solverIterations = 255;
    }

    public void UpdateState()
    {
        //check player is trying to grab
        if (crashTest.IsGrabbingActive)
        {
            animator.SetBool("isGrabbing", true);
        }
        else
        {
            //if we are already carrying something
            if (fixedJoint != null)
            {
                //apply throwing force to connected object on release
                if(fixedJoint.connectedBody != null)
                {
                    float forceAmountMultiplier = 0.8f;



                    fixedJoint.connectedBody.AddForce((crashTest.transform.forward + Vector3.up * 0.25f) * forceAmountMultiplier, ForceMode.Impulse);
                }

                Destroy(fixedJoint);
            }

            //change animation state
            animator.SetBool("isCarrying", false);
            animator.SetBool("isGrabbing", false);
        }
    }

    bool TryCarryObject(Collision collision)
    {
        //check ragdoll is active
        if (!crashTest.IsActiveRagdoll)
            return false;
        //check player is trying to grab
        if (!crashTest.IsGrabbingActive)
            return false;
        //check ragdoll is not already carrying item
        if (fixedJoint != null)
            return false;
        //don't grab self
        if (collision.transform.root == crashTest.transform)
            return false;
        //check item has rigid body
        if (!collision.collider.TryGetComponent(out Rigidbody otherObjectRigidbody))
            return false;

        //if we can pick it up
        fixedJoint = transform.gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = otherObjectRigidbody;

        fixedJoint.autoConfigureConnectedAnchor = false;

        //transform the collision point from world to local space
        fixedJoint.connectedAnchor = collision.transform.InverseTransformPoint(collision.GetContact(0).point);

        animator.SetBool("isCarrying", true);

        return true;

    }


    void OnCollisionEnter(Collision collision)
    {
        TryCarryObject(collision);

    }

    
}
