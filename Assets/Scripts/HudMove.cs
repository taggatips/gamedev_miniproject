using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudMove : MonoBehaviour
{
    public Image arrowUp;
    public Image arrowDown;
    public Image arrowLeft;
    public Image arrowRight;

    private Vector3 arrowUpOriginalPosition;
    private Vector3 arrowDownOriginalPosition;
    private Vector3 arrowLeftOriginalPosition;
    private Vector3 arrowRightOriginalPosition;


    // Start is called before the first frame update
    void Start()
    {
        arrowUpOriginalPosition = arrowUp.transform.position;
        arrowDownOriginalPosition = arrowDown.transform.position;
        arrowLeftOriginalPosition = arrowLeft.transform.position;
        arrowRightOriginalPosition = arrowRight.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Conductor.instance.onBeat())
        {
            arrowUp.transform.position += new Vector3(0, 1, 0);
            arrowDown.transform.position += new Vector3(0, -1, 0);
            arrowLeft.transform.position += new Vector3(-1, 0, 0);
            arrowRight.transform.position += new Vector3(1, 0, 0);

            StartCoroutine(MoveBack());
        }
    }

    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds((float)0.1);
        arrowUp.transform.position = arrowUpOriginalPosition;
        arrowDown.transform.position = arrowDownOriginalPosition;
        arrowLeft.transform.position = arrowLeftOriginalPosition;
        arrowRight.transform.position = arrowRightOriginalPosition;

    }
}
