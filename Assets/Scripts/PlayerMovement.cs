using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    public bool dashing; 

    [Header("Wallrunning")]
    public bool wallrunning;

    public enum MovementState{
        walking,
        wallrunning,
        dashing
    }
    public MovementState state;     
    public CharacterController controller; 
    public float speed = 12f;
    // for checking if player is on ground else gravity velocity will indefinetly icnrease
    public Transform  groundCheck;
    // distance to ground
    public float groundDistance = 0.4f; 
    // prevent the check to register on ground with the player 
    public LayerMask groundMask; 
    public float gravity = -9.81f;

    // stores our current velocity mostly for gravity 
    Vector3 velocity;
    bool isGrounded; 
    float horizontalInput;
    float verticalInput;

    // Update is called once per frame
    
    void Update()
    {
        // Check if grounded in a spehere around the object GroundCheck at the bottom of the player (point, radius, <what to avoid>)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        MovePlayer();
    }

    private void MyInput(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    private void StateHandler(){
        // Mode Dashing
        if (dashing){
            state = MovementState.dashing; 
        }
        // Mode wallrunning
        else if(wallrunning){
            state = MovementState.wallrunning; 
        }
        // Mode walking
        else if(isGrounded){
            state =  MovementState.walking; 
        }
    }

    private void MovePlayer(){
        if(state == MovementState.dashing) return; 
        
        // direction in which we want to move we take transform. right and forward to take the local cordinates aka the relatives and not the absolutes 
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput; 


        // don't aply if is wallrunning 
        if(isGrounded && velocity.y <0 && !wallrunning){
            velocity.y = -2f; // could be 0 but -2 forces player in to the ground just makes things more robust 
        }

        //TODO we can probably do something about this script getting called after the wallrunning one by calling this script from wall running with a delay (invoke)
        if(wallrunning){
            velocity.y = 0f; 
        }

        // calculate falling velocity
        velocity.y += gravity * Time.deltaTime; 
        // delta y = 0.5 *g * t^2
        controller.Move(velocity * Time.deltaTime); 

        
        // speed for speed, Time.deltaTiem to make it framereate independed again 
        controller.Move(move*speed*Time.deltaTime); 


    }

    
   
}
