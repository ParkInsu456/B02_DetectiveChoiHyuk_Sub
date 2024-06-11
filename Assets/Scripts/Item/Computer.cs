using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Computer : MonoBehaviour
{
    public GameObject internetButtonManager; // InternetButton 오브젝트
    private InternetButton internetButtonScript;

    private bool isPlayerNear = false;

    void Start()
    {
        internetButtonScript = internetButtonManager.GetComponent<InternetButton>();
    }

    void Update()
    {
        // 플레이어가 가까이 있을 때만 E 키를 감지
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            internetButtonScript.ToggleInternetUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거 범위에 들어왔을 때
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 트리거 범위를 벗어났을 때
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
