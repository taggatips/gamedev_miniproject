using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; 
    public float speed = 12f; 

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); 

        // direction in which we want to move we take transform. right and forward to take the local cordinates aka the relatives and not the absolutes 
        Vector3 move = transform.right * x + transform.forward * z; 
        // speed for speed, Time.deltaTiem to make it framereate independed again 
        controller.Move(move*speed*Time.deltaTime); 
    }
}
