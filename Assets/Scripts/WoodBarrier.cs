using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBarrier : MonoBehaviour
{
    private Movement Elestral;

    public float size;

    private void Start()
    {
        Elestral = ItemManager.singleton.Elestral;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fireball")
        {
            if (Elestral.fireSize >= size)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
