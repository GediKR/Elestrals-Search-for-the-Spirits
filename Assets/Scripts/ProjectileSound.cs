using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSound : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Sound());
    }

    private IEnumerator Sound()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
