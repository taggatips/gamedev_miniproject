using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSlide : MonoBehaviour
{ 

    [Header("References")]
    //public Transform transform;
    public Transform orientation; 
    public Animation animaton; 

    [Header("Orientation")]
    public Vector3 axis;
    public float distance;
 
    
    [Header("Detection")]
    public LayerMask player;
    public int wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit playerTopHit;
    private bool playerOnTop;

    [Header("Positions")]
    private Vector3 startPosition; 
    private Vector3 targetPosition;
    private float margin; 
    public float tollerance; 
    public float  steps;
    private Vector3 stepDistance; 
    private bool movedThisBeat = false; 
    private bool isReturning = false;
    public bool isMoving = false;
    public Vector3 nextPosition; 

    private bool hasBlinked = false; 
    void Start()
    {
        startPosition = transform.position; 
        targetPosition = transform.position + axis * distance;
        stepDistance = (targetPosition - startPosition) / steps; 
        animaton = GetComponent<Animation>(); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        //animation stuff
        if(Conductor.instance.timeToNextBeat() <= 0.15f && !hasBlinked){
            animaton.Play("blinkVanish");
            print("in blink vanish"); 
            hasBlinked = true; 
        }

        //movement of the obj
        if( Conductor.instance.onBeat() && !movedThisBeat ){
            isMoving = false; 
            movedThisBeat = true; 
            if(Vector3.Distance(targetPosition, transform.position) > Vector3.kEpsilon && !isReturning){
                isMoving = true; 
                transform.position = transform.position + stepDistance;
                nextPosition = stepDistance;  
            }
            else if(Vector3.Distance(startPosition, transform.position) > Vector3.kEpsilon){
                isMoving = true; 
                isReturning = true; 
                transform.position = transform.position - stepDistance;
                nextPosition = -stepDistance; 
            }
            if(Vector3.Distance(startPosition, transform.position) < Vector3.kEpsilon){
                isReturning = false; 
            }
            // needs to be after moving of the block so it can update the player to the same position. 
            if (playerOnTop){
                MovePlayerAlong();
            }
            animaton.Play("blinkAppear");
            hasBlinked = false; 
        }
        else if (!Conductor.instance.onBeat()){
            movedThisBeat = false; 
        }
  
    }
    
    private void CheckForPlayer(){
        playerOnTop = Physics.Raycast(transform.position, orientation.up, out playerTopHit, wallCheckDistance, player);
        //playerOnTop = Physics.BoxCast(transform.position, transform.localScale, transform.up, out playerTopHit, Quaternion.LookRotation(orientation.up), wallCheckDistance, player);
    }

    private void MovePlayerAlong(){
        playerTopHit.transform.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y+0.1f, transform.position.z);
    }
}
