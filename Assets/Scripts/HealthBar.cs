using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float Health;
    private float MaxHealth;

    public GameOverScreen gameOver;
    
    public Heart H1;
    public Heart H2;
    public Heart H3;

    void Start()
    {
        MaxHealth = 3;
        Health = MaxHealth;
    }

    void Update()
    {

    }

    public void Hurt()
    {
        if (Health == 3)
        {
            H1.Hurt();
        }
        else if (Health == 2)
        {
            H2.Hurt();
        }
        else if (Health == 1)
        {
            H3.Hurt();
            StartCoroutine(Dead());
        }
        Health = Health - 1;
    }

    public void Heal()
    {
        if (Health == 2)
        {
            H1.Heal();
        }
        else if (Health == 1)
        {
            H2.Heal();
        }
        Health = Health + 1;
    }

    public void FullHeal()
    {
        H1.Heal();
        H2.Heal();
        H3.Heal();
        Health = MaxHealth;
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.2f);
        gameOver.GameOver();
    }
}
