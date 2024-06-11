using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumPad : MonoBehaviour
{
    public TextMeshProUGUI tmp; //인스펙터 캐싱
    public INumPadable numPadable;
    public string correctNum;   // 이 넘패드의 정답번호

    public Button[] numButtons = new Button[12];    // 0~9 : 숫자, 10 : 지우기, 11 : 완료

    bool isActive = true;

    int remainTime = 3;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        numButtons = GetComponentsInChildren<Button>();
        for (int i = 0; i < 10; i++)
        {
            int index = i; // 캡처링 문제??
            numButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
            numButtons[i].onClick.AddListener( () => BtnAppend(index) );    //인스펙터에서 버튼함수 할당하던걸 코드로 하는것
        }
        //10
        numButtons[10].GetComponentInChildren<TextMeshProUGUI>().text = "Erase";
        numButtons[10].onClick.AddListener(ReActive);
        //11
        numButtons[11].GetComponentInChildren<TextMeshProUGUI>().text = "Enter";
        numButtons[11].onClick.AddListener(Done);
    }

    private void Update()
    {
        if (isActive && tmp.text.Length == 4)
        {
            isActive = false;
            for (int i = 0; i < 10; i++)
            {
                int index = i; // 캡처링 문제??
                numButtons[i].GetComponent<Image>().color = Color.gray;
                numButtons[i].enabled = false;
            }
        }

        if (remainTime == 0)
        {
            // UI종료 후 연출이벤트 실행
            // 카메라 로봇에 주목, 플레이어 입력 모두 안됨, 로봇 색 빨강 고정, 2초 대기후 총발사 효과음, 화면 빨개짐, 게임오버
            CharacterManager.Instance.Player.condition.TakeDamage(1000);
            CloseButton();
        }
    }

    public void BtnAppend(int i)
    {
        tmp.text += i.ToString();
    }


    // 지우기 또는 완료시 버튼 재활성화 및 텍스트 지우기
    void ReActive()
    {
        isActive = true;
        for (int i = 0; i < 10; i++)
        {
            int index = i; // 캡처링 문제??
            numButtons[i].GetComponent<Image>().color = Color.white;
            numButtons[i].enabled = true;
        }
        tmp.text = "";
    }

    // 완료시 정답과 비교
    void Done()
    {
        if(correctNum == tmp.text)
        {
            // 정답 효과음 발생
            numPadable.OnDone();    // 정답효과
        }
        else
        {
            remainTime--;
            Debug.Log($"오답오답오답 남은횟수 : {remainTime}");
        }
    }

    public void CloseButton()
    {
        ReActive();
        gameObject.SetActive(false);
        CharacterManager.Instance.Player.controller.ToggleCursor();
    }

}
