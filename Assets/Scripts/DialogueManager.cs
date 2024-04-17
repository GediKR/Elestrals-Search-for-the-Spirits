using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    private Queue<string> scentences;

    public float talking;
    public bool Ended;

    void Start()
    {
        scentences = new Queue<string>();
        talking = 0;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        talking = 1;

        scentences.Clear();

        foreach (string scentence in dialogue.scentences)
        {
            scentences.Enqueue(scentence);
        }

        DisplayNextScentence();
    }

    public void DisplayNextScentence()
    {
        if (scentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string scentence = scentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeScentence(scentence));
    }

    IEnumerator TypeScentence (string scentence)
    {
        dialogueText.text = "";
        foreach (char letter in scentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        talking = 0;
        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        Ended = true;
        yield return new WaitForSeconds(0.1f);
        Ended = false;
    }
}
