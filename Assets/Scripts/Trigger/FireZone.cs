using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    public float amount;    // 인스펙터에서 설정

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            CharacterManager.Instance.Player.condition.health.MinusCurrent(amount);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }


}
