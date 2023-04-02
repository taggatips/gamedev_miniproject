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

    [Header("CameraEffects")]
    public Camera cam; 
    public float dashFov; 
    private float orginalFov; 

    [Header("Dashing")]
    public float dasSpeed;
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
        cam  = GetComponent<Camera>(); 
    }
     // Update is called once per frame
    void Update()
    {
        //TODO change jump button to dash aka rename Jump 
        //TODO (isGrounded || wallrunning) is isGrounded rly needed anymore since we can dash from walls?
        if(Input.GetButtonDown("Jump") && Conductor.instance.onBeat()){
            StartCoroutine(Dash());
            //TODO as mentioned in the function called
            Invoke(resetDash(), dashDuration + 0.05f);

        }
    }
    IEnumerator Dash(){
        float startTitme = Time.time;
        pm.dashing = true; 
        Vector3 forceToApply = transform.forward * dasSpeed + transform.up * dashUpwardForce;
        // Nathy TODO fov
        while(Time.time < startTitme + dashDuration){
            pm.controller.Move(forceToApply * Time.deltaTime);
            yield return null; 
        }
        


    }

    private string resetDash(){
        //TODO returns string because Invoke in update() wants it todo remove this stuff is ugly af. 
        pm.dashing = false; 
        // Nathy TODO fov
        return "";
    }

   
}
