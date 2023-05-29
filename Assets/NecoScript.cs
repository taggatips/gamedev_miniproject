using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecoScript : MonoBehaviour
{
    [Header("Game Ojects")]
    private Animator animator;
    public Vector3 targetPosition;
    private bool hasSquished = false; 
    private int counter = 0; 
    private Vector3 stepDistance; 
    private Vector3 startPosition; 
    private bool onStart= true; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position; 

        stepDistance = targetPosition - startPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Conductor.instance.onBeat() && !hasSquished){
            if(counter >= 4){
                counter = 0;
                if(onStart){
                    transform.position = transform.position + stepDistance;
                    transform.Rotate(0f,180f,0f);
                    onStart = false; 
                }
                else{
                    transform.position = transform.position - stepDistance;
                    transform.Rotate(0f,180f,0f);
                    onStart = true; 
                }
                
            }else{
            counter++;
            animator.SetTrigger("NecoSquish");
            hasSquished = true; 
            }
        }
        if(!Conductor.instance.onBeat()){
            hasSquished = false; 
        }
        
    }
    
}
