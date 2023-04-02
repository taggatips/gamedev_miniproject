using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject completeLevelUI;

    void Start()
    {
        completeLevelUI = this.transform.GetChild(0).gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelComplete(){
        completeLevelUI.SetActive(true);
    }

    public void HideLevelComplete(){
        completeLevelUI.SetActive(false);
    }

}
