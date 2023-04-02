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

    [Header("Wallrunning")]
    public bool wallrunning;

    public enum MovementState{
        walking,
        wallrunning,
        dashing
    }
    private MovementState state;     
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
    // Update is called once per frame
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); 

        // Check if grounded in a spehere around the object GroundCheck at the bottom of the player (point, radius, <what to avoid>)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // don't aply if is wallrunning 
        if(isGrounded && velocity.y <0 && !wallrunning){
            velocity.y = -2f; // could be 0 but -2 forces player in to the ground just makes things more robust 
        }
        //TODO we can probably do something about this script gettin called after the wallrunning one by calling this script from wall running with a delay (invoke)
        if(wallrunning){
            velocity.y = 0f; 
        }

        // direction in which we want to move we take transform. right and forward to take the local cordinates aka the relatives and not the absolutes 
        Vector3 move = transform.right * x + transform.forward * z; 
        // speed for speed, Time.deltaTiem to make it framereate independed again 
        controller.Move(move*speed*Time.deltaTime); 

        //TODO change jump button to dash aka rename Jump 
        //TODO (isGrounded || wallrunning) is isGrounded rly needed anymore since we can dash from walls?
        // Dash
        if(Input.GetButtonDown("Jump") && (isGrounded || wallrunning) && Conductor.instance.onBeat()){
            //Camera.main.transform.forward; 
            controller.Move(Dash());
        }
        // Normal move
        else{
            // calculate falling velocity
            velocity.y += gravity * Time.deltaTime; 
            print(velocity.y);
            // delta y = 0.5 *g * t^2
            controller.Move(velocity * Time.deltaTime); 
        }

        // Mode - Wallrunning
        //if (wallrunning){
        //    state = MovementState.wallrunning;
        //}

        
        //print("current PlayerPos: " + transform.position);
        //Conductor.instance
    }

    private Vector3 Dash(){
        // resets dash with delay 
        Invoke(nameof(resetDash), dashDuration); 
        Vector3 forceToApply = transform.forward * dashForce + transform.up * dashUpwardForce; 
        return forceToApply; 
        
    }
    private void resetDash(){
        // TODO I think this is no needed in our case also remove the invoke 
    }
}
