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

        if(isGrounded && velocity.y <0 ){
            velocity.y = -2f; // could be 0 but -2 forces player in to the ground just makes things more robust 
        }

        // direction in which we want to move we take transform. right and forward to take the local cordinates aka the relatives and not the absolutes 
        Vector3 move = transform.right * x + transform.forward * z; 
        // speed for speed, Time.deltaTiem to make it framereate independed again 
        controller.Move(move*speed*Time.deltaTime); 

        //TODO change jump button to dash aka rename Jump 
        // Dash
        if(Input.GetButtonDown("Jump") && isGrounded && Conductor.instance.onBeat()){
            //Camera.main.transform.forward; 
            controller.Move(Dash());
        }

        // calculate falling velocity
        velocity.y += gravity * Time.deltaTime; 
        // delta y = 0.5 *g * t^2
        controller.Move(velocity * Time.deltaTime); 
        print("current PlayerPos: " + transform.position);
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
