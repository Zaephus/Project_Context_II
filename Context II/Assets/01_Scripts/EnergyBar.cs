using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class EnergyBar : MonoBehaviour
{
    public int maxWindmills = 12;
    public int windmillAmount = 0;
    float maxHeight = 315.5f;
    public float currentHeight = 0f;
    float width = 35.4f;

    void Update()
    {
        currentHeight = math.remap(0, maxWindmills, 0, maxHeight, windmillAmount);
        transform.localScale = new Vector3(width, currentHeight, 0);
        transform.localPosition = new Vector3(0, ((currentHeight/2) - 155f), 0);
    }
}
