using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    [Header("References")]
    //public Transform transform;
    public Transform orientation; 
    public Animation animaton; 

    private bool hasBlinked = false; 
    // Start is called before the first frame update
    
    void Start()
    {
        animaton = GetComponent<Animation>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Conductor.instance.onBeat() && !hasBlinked){
            animaton.Play("idleBall");
            hasBlinked = true; 
        }
        if(!Conductor.instance.onBeat()){
            hasBlinked = false; 
        }
    }
}
