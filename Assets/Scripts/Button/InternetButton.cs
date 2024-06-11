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
        // 시작 시 모든 UI를 비활성화
        Internet.SetActive(false);
        Mail.SetActive(false);
        SendMail.SetActive(false);
        Blog.SetActive(false);

        // 시작 시 마우스 커서를 숨깁니다.
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
        // 현재 활성화된 UI가 있으면 비활성화
        if (activeUI != null)
        {
            activeUI.SetActive(false);
        }

        // 새로운 UI를 활성화하고 기록
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

    // X 버튼 클릭 시 호출되는 메서드
    public void OnCloseButtonClick()
    {
        HideAllUI();
    }

    // 마우스 커서 상태 업데이트
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








