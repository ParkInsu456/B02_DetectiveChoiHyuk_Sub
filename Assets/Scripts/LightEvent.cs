using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField] private GameObject target;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var lightComponen = target.GetComponent<TurnOffEvent>();
        lightComponen.TurnOffAllLights();
    }
}
