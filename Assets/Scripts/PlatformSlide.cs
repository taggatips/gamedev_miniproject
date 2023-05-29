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
        //animation stuff
        if(Conductor.instance.timeToNextBeat() <= 0.15f && !hasBlinked){
            animaton.Play("blinkVanish");
            hasBlinked = true; 
        }

        //movement of the obj
        if( Conductor.instance.onBeat() && !movedThisBeat ){
            isMoving = false; 
            movedThisBeat = true; 
            if(Vector3.Distance(targetPosition, transform.position) > Vector3.kEpsilon && !isReturning){
                transform.position = transform.position + stepDistance;
                isMoving = true; 
                //calculate if nex position needs to be postivie cuz the platoform changes direction. else the player makes 1 to many negative movements resulting in him falling of. 
                if(Vector3.Distance(targetPosition, transform.position) < Vector3.kEpsilon){
                    nextPosition = -stepDistance;
                }else{
                    nextPosition = stepDistance;
                }
            }
            else if(Vector3.Distance(startPosition, transform.position) > Vector3.kEpsilon){
                isReturning = true; 
                transform.position = transform.position - stepDistance;
                isMoving = true; 
                //calculate if nex position needs to be postivie cuz the platoform changes direction. else the player makes 1 to many negative movements resulting in him falling of. 
                if(Vector3.Distance(startPosition, transform.position) < Vector3.kEpsilon){
                    nextPosition = stepDistance;
                }else{
                    nextPosition = -stepDistance;
                }
            }
            if(Vector3.Distance(startPosition, transform.position) < Vector3.kEpsilon){
                isReturning = false; 
            }
            // needs to be after moving of the block so it can update the player to the same position. 
            animaton.Play("blinkAppear");
            hasBlinked = false; 
        }
        else if (!Conductor.instance.onBeat()){
            movedThisBeat = false; 
        }
  
    }


}
