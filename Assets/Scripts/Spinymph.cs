using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinymph : MonoBehaviour
{
    public Shooter shooter;
    [SerializeField] private float visionRadius;
    [SerializeField] private float hp;
    [SerializeField] private float reloadTime;
    [SerializeField] private LayerMask pLayer;
    [SerializeField] private float ambrosiaDropOdds;
    public Ambrosia ambPrefab;

    private Movement Elestral;

    private float rand;
    private int randInt;

    public AudioSource shoot;

    private void Start()
    {
        Elestral = ItemManager.singleton.Elestral;

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (PlayerClose())
        {
            yield return new WaitForSeconds(reloadTime);
        }
        StartCoroutine(Shoot2());
    }

    private IEnumerator Shoot2()
    {
        if (PlayerClose())
        {
            shooter.Shoot();
            shoot.Play();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Shoot());
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
            if (hp <= 0f)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        rand = Random.Range(1,ambrosiaDropOdds);
        randInt = (int)rand;
        if (randInt== 1)
        {
            Ambrosia ambrosia = Instantiate(ambPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}