using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite one, two, three, four, five;

    public LayerMask destroyLayers;

    public ProjectileSound soundPrefab;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxLifetime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ProjectileSound sound = Instantiate(soundPrefab, transform.position, transform.rotation);
    }

    public void SetSize(float size)
    {
        if (size == 1)
        {
            sr.sprite = one;
        }
        else if (size == 2)
        {
            sr.sprite = two;
        }
        else if (size == 3)
        {
            sr.sprite = three;
        }
        else if (size == 4)
        {
            sr.sprite = four;
        }
        else if (size == 5)
        {
            sr.sprite = five;
        }
    }

    public void Project(Vector3 direction)
    {
        if (direction.x == 1)
        {
            transform.position = new Vector3(transform.position.x + 1.1f, transform.position.y, transform.position.z);
            sr.flipX = false;
        }
        else if (direction.x == -1)
        {
            transform.position = new Vector3(transform.position.x - 1.1f, transform.position.y, transform.position.z);
            sr.flipX = true;
        }
        rb.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTouching())
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsTouching()
    {
        Collider2D destroyCheck = Physics2D.OverlapCircle(transform.position, 0.1f, destroyLayers);
        if(destroyCheck != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}