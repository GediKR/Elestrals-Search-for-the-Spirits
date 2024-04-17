using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public SpiBall shootPrefab;
    private GameObject elestral;
    private Movement Elestral;

    private void Start()
    {
        elestral = GameObject.FindWithTag("Player");
        Elestral = elestral.GetComponent<Movement>();
    }

    private void Update()
    {
        transform.up = elestral.transform.position - transform.position;
    }

    public void Shoot()
    {
        SpiBall spiball = Instantiate(shootPrefab, transform.position, transform.rotation);
        spiball.Project(transform.up);
    }
}