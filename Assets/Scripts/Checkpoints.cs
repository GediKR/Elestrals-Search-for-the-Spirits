using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = this.GetComponent<Renderer>();
        renderer.material.color = Color.clear;
    }
}