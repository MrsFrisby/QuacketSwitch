using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPhysics : MonoBehaviour
{

    Rigidbody rigidBody3D;
    ConfigurableJoint joint;

    [SerializeField]
    Rigidbody animatedRigidBody;

    [SerializeField]
    bool syncAnimation = false;

    //starting rotation
    Quaternion startLocalRotation;

    float startSlerpPositionSpring = 0.0f;


    private void Awake()
    {
        rigidBody3D = GetComponent<Rigidbody>();
        joint = GetComponent<ConfigurableJoint>();

        startLocalRotation = transform.localRotation;
        startSlerpPositionSpring = joint.slerpDrive.positionSpring;
    }

    public void UpdateJointFromAnimation()
    {
        if (!syncAnimation)
            return;
        ConfigurableJointExtensionCT.SetTargetRotationLocal(joint, animatedRigidBody.transform.localRotation, startLocalRotation);
    }

    public void MakeRagdoll()
    {
        JointDrive jointDrive = joint.slerpDrive;
        jointDrive.positionSpring = 1;
        joint.slerpDrive = jointDrive;
    }

    public void MakeActiveRagdoll()
    {
        JointDrive jointDrive = joint.slerpDrive;
        jointDrive.positionSpring = startSlerpPositionSpring;
        joint.slerpDrive = jointDrive;
    }
}
