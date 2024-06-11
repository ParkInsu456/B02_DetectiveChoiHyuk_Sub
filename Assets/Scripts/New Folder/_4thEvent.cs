using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class _4thEvent : MonoBehaviour
{
    public SojaExiles.opencloseDoor door;
    [SerializeField] private Enemy enemy;
    private AudioSource audioSource; // �Ҹ��� ����� AudioSource


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            StartCoroutine(door.opening());
            enemy.SetState(AIState.Running);
            audioSource.Play();
            //Destroy(gameObject);
        }
    }
}
