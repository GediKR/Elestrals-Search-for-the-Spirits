using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpiritUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI earth;
    [SerializeField] public TextMeshProUGUI fire;
    [SerializeField] public TextMeshProUGUI water;
    [SerializeField] public TextMeshProUGUI thunder;
    [SerializeField] public TextMeshProUGUI wind;

    private int num;

    public void Earth()
    {
        int.TryParse(earth.text, out num);
        num = num + 1;
        earth.text = num.ToString();
    }

    public void Fire()
    {
        int.TryParse(fire.text, out num);
        num = num + 1;
        fire.text = num.ToString();
    }

    public void Water()
    {
        int.TryParse(water.text, out num);
        num = num + 1;
        water.text = num.ToString();
    }

    public void Thunder()
    {
        int.TryParse(thunder.text, out num);
        num = num + 1;
        thunder.text = num.ToString();
    }

    public void Wind()
    {
        int.TryParse(wind.text, out num);
        num = num + 1;
        wind.text = num.ToString();
    }
}
