using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElestralAnimation : MonoBehaviour
{
    public SpriteRenderer sr;
    public Movement Elestral;
    public Animator animator;

    private void Update()
    {
        animator.SetBool("Walking", Elestral.Walking);
        animator.SetBool("Jumping", Elestral.Jumping);
        animator.SetBool("Falling", Elestral.Falling);

        if (Elestral.pulling == false)
        {
            if (Elestral.facing == 1)
            {
                sr.flipX = false;
            }
            else if (Elestral.facing == -1)
            {
                sr.flipX = true;
            }
        }
    }
}
