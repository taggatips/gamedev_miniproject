using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float maxWallRuntime;
    private float WallRunTimer;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput; 

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;

    private RaycastHit leftWallHit;
    private RaycastHit rightWalHit; 
    private bool wallLeft;
    private bool wallRight; 

    [Header("References")]
    public Transform orientation; 
    private PlayerMovement pm; 
    private Rigidbody rb; 

    private float originalPlayerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();   
    }
    // Handles the state and how the player should behave. 
    private void StateMachine(){
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallrunning (when the player should enter this state)
        // TODO The (horizontalInput != 0 || verticalInput > 0) is not elegant player should stick to wall if either A,W,D are pressed
        if((wallLeft || wallRight) && horizontalInput != 0  && AboveGround()){
            if(!pm.wallrunning){
                StartWallRun();
            }
        }
        // State 3 - Stop wallrun
        else{
            if(pm.wallrunning){
                StopWallRun();
            }
        }

    } 

    private void FixedUpdate(){
        if(pm.wallrunning){
            WallRunningMovement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        CheckForWall();
    }

    // Check for wals
    private void CheckForWall(){
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWalHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out rightWalHit, wallCheckDistance, whatIsWall);
    }

    // Check if the player is away from the ground so he only gets stuck to walls if there is no platform below him.
    private bool AboveGround(){
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StartWallRun(){
        pm.wallrunning = true; 
    }

    private void WallRunningMovement(){
        // Disable gravity
        pm.gravity = 0f;
        // Set Player speed and save current one to reset after leving wall
        originalPlayerSpeed = pm.speed; 
        pm.speed = 1.5f; 
        // Set y velocity to 0
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // Orientation of the wall, normal is the direction facing away from the wall 
        Vector3 wallNormal = wallRight ? rightWalHit.normal : leftWallHit.normal; 
        // Calculate Orientation of wall with the normal and up. 
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        pm.controller.Move(wallForward * wallRunForce); 
    }

    private void StopWallRun(){
        // Reset Gravity
        pm.gravity = -9.81f;
        // Reset Speed
        pm.speed = originalPlayerSpeed; 
        pm.wallrunning = false; 
    }

}
