using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    private Movement Elestral;

    [SerializeField] private bool isOneTime;
    public Dialogue dialogue;
    private bool triggered;
    public bool Necruff;
    public bool Sluggle;
    public bool Quackle;
    public bool Hydrake;
    public bool DemoEnd;
    private bool timeset;

    private void Start()
    {
        if (Necruff == false && Sluggle == false && Quackle == false && Hydrake == false)
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.color = Color.clear;
        }

        Elestral = ItemManager.singleton.Elestral;

        triggered = false;
        timeset = false;
    }

    private void Update()
    {
        if (triggered == false)
        {
            UnTrigger();
        }

        if(isOneTime == true && FindObjectOfType<DialogueManager>().Ended == true && triggered == true)
        {
            if (Necruff == true)
            {
                Elestral.NecruffButton();
            }
            else if (Sluggle == true)
            {
                Elestral.SluggleButton();
            }
            else if (Quackle == true)
            {
                Elestral.QuackleButton();
            }
            else if (Hydrake == true)
            {
                Elestral.HydrakeButton();
            }
            else if (DemoEnd == true)
            {
                SceneManager.LoadScene("Title");
            }
            UnTrigger();
            Destroy(this.gameObject);
        }
        else if (isOneTime == false && FindObjectOfType<DialogueManager>().Ended == true)
        {
            triggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Player" && triggered == false)
        {
            triggered = true;
            timeset = true;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    private void UnTrigger()
    {
        if (timeset == true)
        {
            timeset = false;
        }
    }
}
