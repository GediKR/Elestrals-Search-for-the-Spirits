using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Movement Elestral;
    public Rigidbody2D rb;
    public FixedJoint2D fj;
    public LayerMask groundLayers;
    public LayerMask Obstacles;
    public Transform feet;
    public Transform returnPos;

    public float weight;
    private bool pushing;
    private bool pushingTest;
    private float weightTemp;

    private Vector3 startPos;

    private float velocity;

    public AudioSource moving;

    private void Start()
    {
        moving.Pause();
        Elestral = ItemManager.singleton.Elestral;
        weightTemp = weight;
        pushing = false;
        StartCoroutine(FreezeStart());
    }

    private void Update()
    {
        if (Elestral.Elestral == "Sproutyr" && Elestral.strength >= weight)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (Elestral.Elestral != "Sproutyr" || Elestral.strength < weight)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (pushing == true && Input.GetKeyDown(KeyCode.R))
        {
            if (CanReturn())
            {
                transform.position = startPos;
            }
            else
            {
                Debug.Log("Cannot Return");
            }
        }

        if (pushing == true && Input.GetKeyDown(KeyCode.E))
        {
            Elestral.pulling = true;
            fj.enabled = true;
            fj.connectedBody = Elestral.rb;
        }
        else if (Input.GetKeyUp(KeyCode.E) || IsGrounded() == false || Elestral.IsGrounded() == false)
        {
            Elestral.pulling = false;
            fj.enabled = false;
        }

        if ((rb.velocity.y < 0.1 && rb.velocity.y > -0.1) && (rb.velocity.x > 0.1 ||rb.velocity.x < -0.1))
        {
            moving.UnPause();
        }
        else
        {
            moving.Pause();
        }

        returnPos.transform.position = startPos;
    }

    private bool CanReturn()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(returnPos.position, 1f, Obstacles);
        if(groundCheck == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, groundLayers);
        if(groundCheck != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Elestral.Elestral == "Sproutyr")
        {
            if (Elestral.strength >= weight)
            {
                pushing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pushing = false;
        }
    }

    private IEnumerator FreezeStart()
    {
        weight = 0;
        yield return new WaitForSeconds(0.5f);
        startPos = new Vector3(transform.position.x, transform.position.y, 0f);
        weight = weightTemp;
    }

}