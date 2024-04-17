using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarSpinosect : MonoBehaviour
{
    public Animator animator;
    public Shooter shooter;
    [SerializeField] private float reloadTime;

    public Spirits spiritPrefab;
    [SerializeField] private Vector3 spiritPosition;

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

    public int maxhp;
    public int hp;

    private Vector3 startPos;

    private Vector3 ElestralPos;

    private float rand;
    private int randInt;

    public AudioSource shoot;

    private void Start()
    {
        hp = maxhp;
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
    }

    private IEnumerator Choose()
    {
        rand = Random.Range(1,4);
        randInt = (int)rand;
        if (randInt == 1)
        {
            StartCoroutine(Move());
        }
        else if (randInt > 1)
        {
            StartCoroutine(Shoot());
        }
        yield return new WaitForSeconds(0.1f);
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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Choose());
    }

    private IEnumerator Shoot()
    {
        if (PlayerClose())
        {
            shooter.Shoot();
            shoot.Play();
            yield return new WaitForSeconds(reloadTime);
        }
        StartCoroutine(Choose());
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
            hp = hp - (int)Elestral.fireSize;
            if (hp <= 0)
            {
                Death();
            }
        }
    }

    public void ResetHealth()
    {
        hp = maxhp;
    }

    private void Death()
    {
        Spirits spirit = Instantiate(spiritPrefab, spiritPosition, Quaternion.Euler(0f,0f,0f));
        Destroy(gameObject);
    }
}