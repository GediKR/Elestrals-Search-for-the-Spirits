using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] public string bossName;
    [SerializeField] public TextMeshProUGUI bossText;
    public StellarSpinosect boss;
    public BossHealthBar bossBar;
    public GameObject bar;

    private bool active;


    void Start()
    {
        active = false;
    }

    void Update()
    {
        if (active)
        {
            bossBar.SetHealth(boss.hp);
        }

        if (boss.hp <= 0f)
        {
            Defeated();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = true;
            bossText.text = bossName;
            bossBar.SetMaxHealth(boss.maxhp);
            bar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boss.ResetHealth();
            active = false;
            bar.SetActive(false);
        }
    }

    private void Defeated()
    {
        active = false;
        bar.SetActive(false);
        Destroy(this.gameObject);
    }
}
