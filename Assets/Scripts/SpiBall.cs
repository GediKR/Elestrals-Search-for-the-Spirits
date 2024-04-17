using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiBall : MonoBehaviour
{
    [SerializeField] private LayerMask destroyLayers;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxLifetime;

    private void Update()
    {
        if (IsTouching())
        {
            Destroy(this.gameObject);
        }
    }

    public void Project(Vector3 direction)
    {
        rb.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifetime);
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