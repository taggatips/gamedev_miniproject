using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void onCollision(){
        print("in onCollision");
    } 

    void OnCollisionEnter(Collision collision){
        print("in OnCollisionEnter");
    }

    void OnTriggerEnter(){
        print("ontrigger");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
