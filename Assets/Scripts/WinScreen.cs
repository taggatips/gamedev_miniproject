using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void Start()
    {
        scoreText.text = "Score: " + LevelManager.score;
        StartCoroutine(LoadNextLevel());
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("VisualTest");
    }
}
