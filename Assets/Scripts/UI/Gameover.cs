using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gameover : MonoBehaviour
{
    private bool isInputDisabled = false;

    private void OnEnable()
    {
        ToggleDisableInput();
    }

    private void OnDisable()
    {
        ToggleDisableInput();
    }

    // 활성화시 움직임 입력 막기
    void ToggleDisableInput()
    {
        if (!isInputDisabled)
        { 
            CharacterManager.Instance.Player.GetComponent<PlayerInput>().actions.Disable();
            isInputDisabled = true;
        }
        else
        {
            CharacterManager.Instance.Player.GetComponent<PlayerInput>().actions.Enable();
            isInputDisabled = false;
        }
    }

    public void ReturnCheckPoint()
    {
        CharacterManager.Instance.Player.condition.IsDieFalse();
        CheckPointManager.instance.ExecuteLoad();
        ToggleUI();
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ToggleUI() 
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            if(Cursor.lockState == CursorLockMode.Locked)
            CharacterManager.Instance.Player.controller.ToggleCursor();
        }
        else
        {
            gameObject.SetActive(false);            
            CharacterManager.Instance.Player.controller.ToggleCursor();
        }
    }
}
