using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject about;
    public GameObject credits;
    
    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void AboutButton()
    {
        about.SetActive(true);
    }

    public void AboutBack()
    {
        about.SetActive(false);
    }

    public void CreditsButton()
    {
        credits.SetActive(true);
    }

    public void CreditsBack()
    {
        credits.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
