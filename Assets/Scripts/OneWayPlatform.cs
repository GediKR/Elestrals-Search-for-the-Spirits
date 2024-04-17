using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            StartCoroutine(DropDown());
        }
    }

    private IEnumerator DropDown()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
