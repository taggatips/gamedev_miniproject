using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabing : MonoBehaviour
{
    // Start is called before the first frame updaten
    [Header("References")]
    public PlayerMovement pm; 
    public Transform orientation; 
    public Transform cam; 
    public Rigidbody rb; 

    [Header("Wall Detection")]
    public float wallDetectionLength;
    public float wallSphereCastRadius; 
    public LayerMask whatIsWall; 

    private RaycastHit wallHit;
    
    [Header("Wall grabing")]
    public float movetoWallSpeed; 
    public float maxWallGrabDistance;
    public float minTimeOnWall; 
    private float timeOnWall;
    public bool holding; 
     
    private void WallDetection(){
        //detect wall
        bool wallDetect = Physics.SphereCast(transform.position, wallSphereCastRadius, cam.forward, out wallHit, wallDetectionLength, whatIsWall);
        if(!wallDetect) return; 
        //
        float distanceToWall = Vector3.Distance(transform.position, wallHit.transform.position); 

        // Enter wall hold
        if(distanceToWall < maxWallGrabDistance && !holding) EnterWallHold();
    }   

    private void StateMachine(){
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool anyInputKeyPressed = horizontalInput != 0 || verticalInput != 0; 

        // State 1 Holding on to wall
        if (holding){
            //TODO does nothing atm
            FreezeRigidbodyOnWall();
            timeOnWall += Time.deltaTime; 
        }
    }
    private void EnterWallHold(){
        holding = true; 
        pm.gravity = 0; 
    }

    private void FreezeRigidbodyOnWall(){

    }

    private void ExitWall(){

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     WallDetection();   
    }
}
