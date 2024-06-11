using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEvent : MonoBehaviour
{
    [SerializeField] GameObject gameClear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            gameClear.SetActive(true);
        }
    }
}
