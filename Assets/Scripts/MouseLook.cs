using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class MouseLook : MonoBehaviour
{
    //speed of mouse
    public float mouseSensitivety = 100f; 
    // the model to be rotated 
    public Transform playerBody;
    // rotation around x axis to look up and down
    float xRotation = 0f;  
    // Start is called before the first frame update
    void Start()
    {
        // hides and locks the cursor to the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime is for making sure we are not rotating faster or slower based on the frame rate 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivety * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivety * Time.deltaTime;
        
        // decrase because it is what it is and it serves to either look up or down 
        xRotation -= mouseY; 
        // makes sure we cant look further up and down than 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
        // rotate magic for up and down 
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // rotates the player around the y axis 
        playerBody.Rotate(Vector3.up * mouseX); 
    }

    public void DoFov(float endValue){
        GetComponent<Camera>().DOFieldOfView(endValue,0.1f);
    }
}
