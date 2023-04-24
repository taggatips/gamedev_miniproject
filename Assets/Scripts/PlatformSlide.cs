using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSlide : MonoBehaviour
{ 

    [Header("References")]
    //public Transform transform;

    [Header("Orientation")]
    public Vector3 axis;
    public float distance;
    public double speed;

    [Header("Positions")]
    private Vector3 startPosition; 
    private Vector3 targetPosition;
    private float margin; 
    public float tollerance; 
    public float  steps;
    private Vector3 stepDistance; 
    private bool movedThisBeat = false; 
    private bool isReturning = false; 
    void Start()
    {
        startPosition = transform.position; 
        targetPosition = transform.position + axis * distance;
        stepDistance = (targetPosition - startPosition) / steps; 
    }

    // Update is called once per frame
    void Update()
    {
        if( Conductor.instance.onBeat() && !movedThisBeat ){
            movedThisBeat = true; 
            if(Vector3.Distance(targetPosition, transform.position) > Vector3.kEpsilon && !isReturning){
                transform.position = transform.position + stepDistance; 
            }
            else if(Vector3.Distance(startPosition, transform.position) > Vector3.kEpsilon){
                isReturning = true; 
                transform.position = transform.position - stepDistance; 
            }
            if(Vector3.Distance(startPosition, transform.position) < Vector3.kEpsilon){
                isReturning = false; 
            }
        }
        else if (!Conductor.instance.onBeat()){
            movedThisBeat = false; 
        }
    }
}
