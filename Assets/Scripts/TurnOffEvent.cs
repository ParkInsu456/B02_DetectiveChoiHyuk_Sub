using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffEvent : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    private int lightLen;
    private int lightCnt;

    private void Start()
    {
        lightLen = lights.Length;
        lightCnt = 0;
    }

    public void TurnOffAllLights()
    {
        foreach (Light light in lights) light.intensity = 0;
    }

    public void TurnOnAllLights()
    {
        foreach (Light light in lights) light.intensity = 1;
    }

    public void TurnOffSequentially()
    {

    }
}
