using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.SetBool("Hurt", false);
    }

    void Update()
    {
        
    }

    public void Hurt()
    {
        animator.SetBool("Hurt", true);
    }

    public void Heal()
    {
        animator.SetBool("Hurt", false);
    }
}
