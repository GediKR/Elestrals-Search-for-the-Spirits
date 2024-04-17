using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Movement Elestral;
    public Pausing Paused;
    public GameObject Screen;
    public AudioSource theme;
    public AudioSource pause;

    public void GameOver()
    {
        theme.Stop();
        pause.Play();
        Paused.paused = true;
        Screen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        theme.Play();
        pause.Stop();
        Paused.paused = false;
        Screen.SetActive(false);
        Time.timeScale = 1f;
        Elestral.Checkpoint();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
