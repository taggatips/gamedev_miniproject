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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
            Scene scene = SceneManager.GetActiveScene();
            StartCoroutine(LoadWinScreen());
            if (scene.name == "FirstLevel")
            {
                StartCoroutine(LoadNextLevel("VisualTest"));
            }
            if( scene.name == "TutorialLevel")
            {
                StartCoroutine(LoadNextLevel("FirstLevel"));
            }
        }
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("WinScreen");
    }

    IEnumerator LoadNextLevel(string level)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(level);
    }
}
