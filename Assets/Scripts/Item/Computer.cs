using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Computer : MonoBehaviour
{
    public GameObject internetButtonManager; // InternetButton ������Ʈ
    private InternetButton internetButtonScript;

    private bool isPlayerNear = false;

    void Start()
    {
        internetButtonScript = internetButtonManager.GetComponent<InternetButton>();
    }

    void Update()
    {
        // �÷��̾ ������ ���� ���� E Ű�� ����
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            internetButtonScript.ToggleInternetUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���� ������ ������ ��
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ Ʈ���� ������ ����� ��
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
