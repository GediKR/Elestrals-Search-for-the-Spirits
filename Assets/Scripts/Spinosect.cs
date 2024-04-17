using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinosect : MonoBehaviour
{
    public SpriteRenderer sr;

    public Movement Elestral;

    private float left;
    private float right;
    private float top;
    private float bottom;
    private float facing;
    [SerializeField] private float distance;
    [SerializeField] private float height;
    [SerializeField] private float pause;
    [SerializeField] private float pause2;
    [SerializeField] private float speed;
    [SerializeField] private float speed2;
    [SerializeField] private float attackPause;
    [SerializeField] private float visionRadius;
    private Vector3 velocity = Vector3.zero;

    public LayerMask pLayer;

    private float xPos;
    private float yPos;

    private float tempX;
    private float tempY;
    private float currentPosX;
    private float currentPosY;
    
    private bool attacking;

    private Vector3 newPosition;

    private void Start()
    {
        left = transform.position.x + (distance/2);
        right = transform.position.x - (distance/2);
        top = transform.position.y + (height/2);
        bottom = transform.position.y - (height/2);
        StartCoroutine(Float());
        StartCoroutine(Move());
        StartCoroutine(PlayerChase());
    }

    private void Update()
    {
        newPosition = new Vector3(xPos, yPos, transform.position.z);
        if (PlayerClose() == false)
        {
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, speed);
        }
        else if (PlayerClose())
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY, transform.position.z), ref velocity, speed);
        }

        if (facing == 0)
        {
            sr.flipX = false;
        }
        else if (facing == 1)
        {
            sr.flipX = true;
        }

        if (PlayerClose() && attacking == false)
        {
            tempX = transform.position.x;
            tempY = transform.position.y;
            currentPosX = transform.position.x;
            currentPosY = transform.position.y;
            attacking = true;
        }

        if (PlayerClose() == false)
        {
            attacking = false;
        }
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
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }



    private IEnumerator Move()
    {
        if (PlayerClose() == false)
        {
            facing = 1;
            xPos = left;
            yield return new WaitForSeconds(pause2);
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Move2());
    }

    private IEnumerator Move2()
    {
        if (PlayerClose() == false)
        {
            facing = 0;
            xPos = right;
            yield return new WaitForSeconds(pause2);
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Move());
    }


    private IEnumerator Float()
    {
        if (PlayerClose() == false)
        {
            yPos = top;
            yield return new WaitForSeconds(pause);
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Float2());
    }

    private IEnumerator Float2()
    {
        if (PlayerClose() == false)
        {
            yPos = bottom;
            yield return new WaitForSeconds(pause);
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Float());
    }


    private IEnumerator PlayerChase()
    {
        if (PlayerClose())
        {
            currentPosX = tempX;
            currentPosY = tempY;
            yield return (currentPosX, currentPosY);
        }
        StartCoroutine(PlayerChase2());
    }

    private IEnumerator PlayerChase2()
    {
        if (PlayerClose())
        {
            Debug.Log("Wait");
            yield return new WaitForSeconds(attackPause);
            tempX = Elestral.transform.position.x;
            tempY = Elestral.transform.position.y;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PlayerChase());
    }
}
