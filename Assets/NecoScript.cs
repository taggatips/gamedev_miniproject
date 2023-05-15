using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecoScript : MonoBehaviour
{
    [Header("Game Ojects")]
    private Animator animator;
    private bool hasSquished = false; 
    private int counter = 0; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Conductor.instance.onBeat() && !hasSquished){
            if((counter % 4) == 0){
                counter = 0;
                //transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

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
