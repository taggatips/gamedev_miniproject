using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI scoreText;
    /* TODO : make LevelManager static and undestroyable
    private static LevelManager _instance;

    public static LevelManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            UpdateScore(1);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
            StartCoroutine(WaitAndLoadWinScreen());
            //StartCoroutine(LoadNextLevel("VisualTest"));
        }
    }

    IEnumerator WaitAndLoadWinScreen()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("WinScreen");
    }

    IEnumerator LoadNextLevel(string level)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(level);
    }
}
