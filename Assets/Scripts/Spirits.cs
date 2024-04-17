using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirits : MonoBehaviour
{
    private Movement Elestral;
    private float Xpos;
    private float top;
    private float bottom;
    [SerializeField] private float height;
    [SerializeField] private float pause;
    [SerializeField] private float speed;
    private Vector3 velocity = Vector3.zero;

    public float element;

    private Vector3 newPosition;

    private void Start()
    {
        Elestral = ItemManager.singleton.Elestral;
        Xpos = transform.position.x;
        top = transform.position.y + (height/2);
        bottom = transform.position.y - (height/2);
        StartCoroutine(Float());
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (element == 1)
            {
                Elestral.EarthSpirit();
            }
            else if (element == 2)
            {
                Elestral.FireSpirit();
            }
            else if (element == 3)
            {
                Elestral.WaterSpirit();
            }
            else if (element == 4)
            {
                Elestral.ThunderSpirit();
            }
            else if (element == 5)
            {
                Elestral.WindSpirit();
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator Float()
    {
        newPosition = new Vector3(transform.position.x, top, transform.position.z);
        yield return new WaitForSeconds(pause);
        StartCoroutine(Float2());
    }

    private IEnumerator Float2()
    {
        newPosition = new Vector3(transform.position.x, bottom, transform.position.z);
        yield return new WaitForSeconds(pause);
        StartCoroutine(Float());
    }
}