using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InternetButton : MonoBehaviour
{
    public GameObject Internet;
    public GameObject Mail;
    public GameObject SendMail;
    public GameObject Blog;

    private GameObject activeUI;
    private bool isInternetVisible = false;

    void Start()
    {
        // ���� �� ��� UI�� ��Ȱ��ȭ
        Internet.SetActive(false);
        Mail.SetActive(false);
        SendMail.SetActive(false);
        Blog.SetActive(false);

        // ���� �� ���콺 Ŀ���� ����ϴ�.
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleInternetUI()
    {
        isInternetVisible = !isInternetVisible;
        Internet.SetActive(isInternetVisible);
        if (!isInternetVisible)
        {
            HideAllUI();
        }
        UpdateCursorState();
    }

    public void ShowInternetUI()
    {
        ShowUI(Internet);
    }

    public void ShowMailUI()
    {
        ShowUI(Mail);
    }

    public void ShowSendMailUI()
    {
        ShowUI(SendMail);
    }

    public void ShowBlogUI()
    {
        ShowUI(Blog);
    }

    private void ShowUI(GameObject ui)
    {
        // ���� Ȱ��ȭ�� UI�� ������ ��Ȱ��ȭ
        if (activeUI != null)
        {
            activeUI.SetActive(false);
        }

        // ���ο� UI�� Ȱ��ȭ�ϰ� ���
        ui.SetActive(true);
        activeUI = ui;
        UpdateCursorState();
    }

    public void HideAllUI()
    {
        Internet.SetActive(false);
        Mail.SetActive(false);
        SendMail.SetActive(false);
        Blog.SetActive(false);
        activeUI = null;
        UpdateCursorState();
    }

    // X ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnCloseButtonClick()
    {
        HideAllUI();
    }

    // ���콺 Ŀ�� ���� ������Ʈ
    private void UpdateCursorState()
    {
        if (isInternetVisible || activeUI != null)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}








