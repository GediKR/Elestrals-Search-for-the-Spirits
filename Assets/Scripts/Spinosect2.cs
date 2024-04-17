using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinosect2 : MonoBehaviour
{
    public Animator animator;

    public SpriteRenderer sr;
    private float currentPosX;
    private float currentPosY;
    private float startingPosX;
    private float startingPosY;
    [SerializeField] private float speed;
    private Vector3 velocity = Vector3.zero;
    private Movement Elestral;

    [SerializeField] private float visionRadius;

    private float playerX;
    private float playerY;

    private float tempX;
    private float tempY;

    public LayerMask pLayer;

    public float hp;

    private Vector3 startPos;

    private Vector3 ElestralPos;

    public AudioSource Buzz;

    private void Start()
    {
        animator.SetBool("Flying", true);
        startPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

        Elestral = ItemManager.singleton.Elestral;

        startingPosX = transform.position.x;
        startingPosY = transform.position.y;

        currentPosX = transform.position.x;
        currentPosY = transform.position.y;

        tempX = currentPosX;
        tempY = currentPosY;
        StartCoroutine(Move());
    }
    
    private void Update()
    {
        playerX = Elestral.transform.position.x;
        playerY = Elestral.transform.position.y;

        if (animator.GetBool("Flying") == false)
        {
            transform.right = ElestralPos - transform.position;
        }
        else
        {
            transform.right = new Vector3(0f, 0f, 0f);
        }

        if (PlayerClose())
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY, transform.position.z), ref velocity, speed);
        }

        if (animator.GetBool("Flying") == false)
        {
            if (ElestralPos.x > transform.position.x)
            {
                sr.flipX = true;
                sr.flipY = false;
            }
            else if (ElestralPos.x < transform.position.x)
            {
                sr.flipX = true;
                sr.flipY = true;
            }
        }
        else if (animator.GetBool("Flying") == true)
        {
            if (Elestral.transform.position.x > transform.position.x)
            {
                sr.flipX = true;
                sr.flipY = false;
            }
            else if (Elestral.transform.position.x < transform.position.x)
            {
                sr.flipX = false;
                sr.flipY = false;
            }
        }

        if (Elestral.paused == true)
        {
            Buzz.Pause();
        }
        else
        {
            Buzz.UnPause();
        }

    }

    private IEnumerator Move()
    {
        if (PlayerClose())
        {
            animator.SetBool("Flying", false);
            ElestralPos = Elestral.transform.position;
            tempX = playerX;
            tempY = playerY;
            yield return new WaitForSeconds(0.5f);
            currentPosX = tempX;
            currentPosY = tempY;
            yield return new WaitForSeconds(1f);
            animator.SetBool("Flying", true);
            currentPosX = startingPosX;
            currentPosY = startingPosY;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(Move2());
    }

    private IEnumerator Move2()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Move());
    }

    private IEnumerator Move3()
    {
        if (PlayerClose())
        {
            currentPosX = tempX;
            currentPosY = tempY;
            yield return (currentPosX, currentPosY);
        }
        StartCoroutine(Move2());
    }

    private IEnumerator Move4()
    {
        if (PlayerClose())
        {
            yield return new WaitForSeconds(1f);
            animator.SetBool("Flying", true);
            currentPosX = startingPosX;
            currentPosY = startingPosY;
            yield return new WaitForSeconds(2.5f);
            animator.SetBool("Flying", false);
            ElestralPos = Elestral.transform.position;
            tempX = playerX;
            tempY = playerY;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Move());
    }

    public bool PlayerClose()
    {
        Collider2D playerCheck = Physics2D.OverlapCircle(transform.position, visionRadius, pLayer);
        if(playerCheck != null)
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
        if (collision.tag == "Fireball")
        {
            hp = hp - Elestral.fireSize;
            if (hp <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}