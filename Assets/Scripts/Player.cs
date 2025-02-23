//https://www.youtube.com/watch?v=Weu305NLMqo&list=PLyDa4NP_nvPeSosMrZ0Gv03v5s4k7Le7N&index=1
//Made following active ragdoll tutorials by Pretty Fly Games
//and Learn Unity Beginner?Intermediate 2024: code Monkey
//https://www.youtube.com/watch?v=AmGSEH7QcDg&t=6477s
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDuckObjectParent
{
    //singleton pattern
    public static Player Instance { get; private set; }

    public event EventHandler OnImpact;
    public event EventHandler OnRevive;
    public event EventHandler OnPickUp;
    public event EventHandler <OnSelectedPalletChangedEventArgs> OnSelectedPalletChanged;
    public class OnSelectedPalletChangedEventArgs : EventArgs
    {
        public BasePallet selectedPallet;
    }

    [SerializeField]
    Rigidbody rigidBody3D;

    [SerializeField]
    ConfigurableJoint mainJoint;

    [SerializeField]
    Animator animator;

    [SerializeField]
    public GameInput gameInput;

    [SerializeField]
    private int reviveTime;

    //Input
    Vector2 moveInputVector = Vector2.zero;
    bool isJumpButtonPressed = false;
    bool isRevivedButtonPressed = false;
    bool isGrabPressed = false;
    //bool isInteractPressed = false;


    //Controller
    [SerializeField] float maxSpeed = 4;

    //States
    bool isGrounded = false;
    bool isActiveRagdoll = true;
    public bool IsActiveRagdoll => isActiveRagdoll;
    bool isGrabbingActive = false;
    public bool IsGrabbingActive => isGrabbingActive;
    bool isMoving = false;
    public bool IsMoving => isMoving;
    public bool isTPressed = false;
    public bool isEPressed = false;

    //Raycasts
    RaycastHit[] raycastHits = new RaycastHit[10];

    //sync animation to ragdoll
    SyncPhysics[] syncPhysicsObjects;

    //store original values
    float startSlerpPositionSpring = 0.0f;

    //timing
    float lastTimeBecameRagdoll = 0.0f;


    //grabHandlers
    HandGrabHandler[] handGrabHandlers;

    //interaction

    Vector3 lastInderactDir;
    [SerializeField]
    private LayerMask interactLayerMask;
    private BasePallet selectedPallet;
    private DuckObject duckObject;

    //public DuckObject grabbedDuck = null;

    [SerializeField]
    private Transform duckHoldPoint;
    //don't want to use this as already have grab mechanics

    [SerializeField]
    private Transform assembledDuckHoldPoint;

    private void Awake()
    {
        syncPhysicsObjects = GetComponentsInChildren<SyncPhysics>();
        handGrabHandlers = GetComponentsInChildren<HandGrabHandler>();

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startSlerpPositionSpring = mainJoint.slerpDrive.positionSpring;

        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractCancelAction += GameInput_OnInteractCancelAction;
        gameInput.OnReviveAction += GameInput_OnReviveAction;
        gameInput.OnReviveCancelAction += GameInput_OnReviveCancelAction;
        gameInput.OnGrabAction += GameInput_OnGrabAction;
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnGrabCancelAction += GameInput_OnGrabCancelAction;
        gameInput.OnInteractTutorialAction += GameInput_OnInteractTutorialAction;
        gameInput.OnInteractTutorialCancelAction += GameInput_OnInteractTutorialCancelAction;

    }

    private void GameInput_OnInteractCancelAction(object sender, EventArgs e)
    {
        isEPressed = false;
    }



    //pressing T key in Tutorial scene
    private void GameInput_OnInteractTutorialAction(object sender, EventArgs e)
    {
        isTPressed = true;
    }

    private void GameInput_OnInteractTutorialCancelAction(object sender, EventArgs e)
    {
        isTPressed = false;
    }

    //grabbing
    private void GameInput_OnGrabAction(object sender, System.EventArgs e)
    {
        //Debug.Log("Inside Player controller: G");
        isGrabPressed = true;
    }

    private void GameInput_OnGrabCancelAction(object sender, System.EventArgs e)
    {
        //Debug.Log("Inside Player controller: G released");
        isGrabPressed = false;
    }

    //jump
    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        //Debug.Log("Inside Player controller: Space");
        isJumpButtonPressed = true;
    }

    
    //revive
    private void GameInput_OnReviveCancelAction(object sender, System.EventArgs e)
    {
        //Debug.Log("Inside Player controller: R released");
        isRevivedButtonPressed = false;
        //Debug.Log(isRevivedButtonPressed);
    }

    private void GameInput_OnReviveAction(object sender, System.EventArgs e)
    {
        //Debug.Log("Inside Player controller: R");
        isRevivedButtonPressed = true;
        
    }

    //interact
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying())
        {//Player can't interact unless in playing gameState
            return;
        };
        isEPressed = true;
        if (selectedPallet != null)
        {
            selectedPallet.Interact(this);
        }

    }

    void Update()
    {
        moveInputVector = gameInput.GetMovementVectorNormalized();

        if(isGrabPressed)
        {
            animator.SetBool("isGrabbing", true);
        }

        HandleInteractions();

        ////Test for collisions
        //if (isTPressed)
        //{
        //    float interactRange = 0.5f;
        //    Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, LayerMask.GetMask("Interact"));
        //    foreach (Collider collider in colliderArray)
        //    {
        //        if (collider.TryGetComponent(out TutorialInteractable tutorialInteractable))
        //        {
        //            tutorialInteractable.Interact();
        //        }
        //    }
        //}

    }

    public TutorialInteractable GetTutorialInteractable()
    {
        float interactRange = 0.5f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, LayerMask.GetMask("Interact"));
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out TutorialInteractable tutorialInteractable))
            {
                return tutorialInteractable;
            }
        }
        return null;
    }

    void FixedUpdate()
    {
        //assume character is not grounded
        isGrounded = false;

        //check if grounded
        int numberOfHits = Physics.SphereCastNonAlloc(rigidBody3D.position, 0.1f, transform.up * -1, raycastHits, 0.5f);

        //check for valid hits
        for (int i = 0; i < numberOfHits; i++)
        {
            //ignore self hits
            if (raycastHits[i].transform.root == transform)
                continue;

            isGrounded = true;

            break;
        }

        //extra gravity when falling
        if (!isGrounded)
            rigidBody3D.AddForce(Vector3.down * 10);

        float inputMagnitude = moveInputVector.magnitude;

        //check not exceeding max speed
        Vector3 localVelocityVsForward = transform.forward * Vector3.Dot(transform.forward, rigidBody3D.velocity);
        float localForwardVelocity = localVelocityVsForward.magnitude;

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0f,2f,0f);
            MakeActiveRagdoll();
        }

        //if G key held down set grabbingActive bool to true
        isGrabbingActive = isGrabPressed;

        foreach (HandGrabHandler handGrabHandler in handGrabHandlers)
        {
            //for each hand constantly update to check if object has been grabbed
            handGrabHandler.UpdateState();
        }

        

        if (isActiveRagdoll)
        {
            if (inputMagnitude != 0)
            {
                isMoving = true;

                //look in direction of movement
                Quaternion desiredDirection = Quaternion.LookRotation(new Vector3(moveInputVector.x, 0, moveInputVector.y * -1), transform.up);

                //rotate body to direction of movement
                mainJoint.targetRotation = Quaternion.RotateTowards(mainJoint.targetRotation, desiredDirection, Time.fixedDeltaTime * 300);



                if (localForwardVelocity < maxSpeed)
                {
                    //move forward
                    rigidBody3D.AddForce(transform.forward * inputMagnitude * 30);
                }

                
            }
            else
            {
                isMoving = false;
            }

            if (isGrounded && isJumpButtonPressed)
            {
                rigidBody3D.AddForce(Vector3.up * 20, ForceMode.Impulse);
                isJumpButtonPressed = false;
            }

            if (isTPressed)
                //if (isGrounded && isTPressed)
            {
                rigidBody3D.AddForce(moveInputVector * -20, ForceMode.Impulse);
                isTPressed = false;
            }

            if (isRevivedButtonPressed)
            {
                rigidBody3D.AddForce(moveInputVector * 20, ForceMode.Impulse);
                isRevivedButtonPressed = false;
            }

        }
        else
        {
            if(isRevivedButtonPressed && (Time.time - lastTimeBecameRagdoll) > reviveTime )
            {
                MakeActiveRagdoll();
            }
        }

        //make speed of animation match movement speed
        animator.SetFloat("movementSpeed", localForwardVelocity *0.4f);


        //Update joint rotations
        for (int i = 0; i< syncPhysicsObjects.Length; i++)
        {
            syncPhysicsObjects[i].UpdateJointFromAnimation();
        }

        

    }
    //end of FixedUpdate

    public void OnBodyPartHit()
    {
        if (!isActiveRagdoll)
            return;
        MakeRagdoll();
    }


    void MakeRagdoll()
    {
        OnImpact?.Invoke(this, EventArgs.Empty);
        //Update main joint
        JointDrive jointDrive = mainJoint.slerpDrive;
        jointDrive.positionSpring = 0;
        mainJoint.slerpDrive = jointDrive;

        //update all joints
        for (int i = 0; i < syncPhysicsObjects.Length; i++)
        {
            syncPhysicsObjects[i].MakeRagdoll();
        }

        lastTimeBecameRagdoll = Time.time;
        isActiveRagdoll = false;
        isGrabbingActive = false;
    }

    void MakeActiveRagdoll()
    {
        OnRevive?.Invoke(this, EventArgs.Empty);
        //Update main joint
        JointDrive jointDrive = mainJoint.slerpDrive;
        jointDrive.positionSpring = startSlerpPositionSpring;
        mainJoint.slerpDrive = jointDrive;

        //update all joints
        for (int i = 0; i < syncPhysicsObjects.Length; i++)
        {
            syncPhysicsObjects[i].MakeActiveRagdoll();
        }

        isActiveRagdoll = true;
        isGrabbingActive = false;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(-inputVector.x, 0, -inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInderactDir = moveDir;
        }

        float interactDistance = 2f;

        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

        Debug.DrawRay(rayOrigin, 10f * lastInderactDir, Color.red);

        //raycast will return info about objects in front of player even if player is not moving
        if (Physics.Raycast(rayOrigin, lastInderactDir, out RaycastHit raycastHit, interactDistance, interactLayerMask))
        {
            if (raycastHit.transform.tag != "ownBody" && raycastHit.transform != gameObject.transform)
            {
                Debug.DrawRay(rayOrigin, 10f * lastInderactDir, Color.green);
                if (raycastHit.transform.TryGetComponent(out BasePallet basePallet))
                {
                    if (basePallet != selectedPallet)
                    {
                        SetSelectedPallet(basePallet);
                        //Debug.Log("I hit a " + raycastHit.transform.name);
                    }
                }
                //else if (isTPressed && raycastHit.transform.TryGetComponent(out TutorialInteractable tutorialInteractable))
                //{
                //    tutorialInteractable.Interact();
                //}

                else
                {
                    SetSelectedPallet(null);
                    //Debug.Log("Not a pallet: "+raycastHit.transform.name);
                }                    
            }
            else
            {
                SetSelectedPallet(null);
                //Debug.Log("Not a pallet: " + raycastHit.transform.name);
            }
        }
    }

    private void SetSelectedPallet(BasePallet selectedPallet)
    {
        this.selectedPallet = selectedPallet;
        //Debug.Log("I have selected a " +selectedPallet);

        OnSelectedPalletChanged?.Invoke(this, new OnSelectedPalletChangedEventArgs
        {
            selectedPallet = selectedPallet           
        });
    }

    //Player script: implement IDuckObjectParent interface functions

    public Transform GetDuckObjectFollowTransform()
    {
        return duckHoldPoint;
    }

    public Transform GetAssembledDuckObjectFollowTransform()
    {
        return assembledDuckHoldPoint;
    }

    public void SetDuckObject(DuckObject duckObject)
    {
        this.duckObject = duckObject;

        if (duckObject != null)
        {
            OnPickUp?.Invoke(this, EventArgs.Empty);
            animator.SetBool("isHolding", true);
        }
            
    }

    public DuckObject GetDuckObject()
    {
        return duckObject;
    }

    public void ClearDuckObject()
    {
        duckObject = null;
        animator.SetBool("isHolding", false);
    }

    public bool HasDuckObject()
    {
        return duckObject != null;
    }


}



