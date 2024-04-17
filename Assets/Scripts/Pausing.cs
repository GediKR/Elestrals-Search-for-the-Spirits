using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Pausing : MonoBehaviour
{
    public GameObject PauseScreen;
    public bool paused;
    public AudioSource theme;
    public AudioSource pause;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        theme.Pause();
        pause.Play();
        paused = true;
        PauseScreen.SetActive(true);
        StartCoroutine(Pause2());
    }

    public void UnPause()
    {
        theme.UnPause();
        pause.Stop();
        paused = false;
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    private void FUCKINGPAUSE()
    {
        pause.Pause();
    }

    private IEnumerator Pause2()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
    }


}
