using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager singleton;

    public Movement Elestral;

    protected void Awake()
    {
        singleton = this;
    }
}
