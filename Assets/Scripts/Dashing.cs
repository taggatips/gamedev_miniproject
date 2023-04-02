using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation; 
    public Transform playerCam;
    private PlayerMovement pm;
    private Rigidbody rb; 

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();   
    }
     // Update is called once per frame
    void Update()
    {
        //TODO change jump button to dash aka rename Jump 
        //TODO (isGrounded || wallrunning) is isGrounded rly needed anymore since we can dash from walls?
        if(Input.GetButtonDown("Jump") && Conductor.instance.onBeat())Dash(); 
    }
    private void Dash(){
        // set state do dash
        pm.dashing = true; 
        Vector3 forceToApply = transform.forward * dashForce + transform.up * dashUpwardForce; 
        pm.controller.Move(forceToApply);
        // resets dash with delay 
        Invoke(nameof(resetDash), dashDuration);  
    
    }

     private void resetDash(){
        // TODO I think this is no needed in our case also remove the invoke
        pm.dashing = false; 
    }

   
}
