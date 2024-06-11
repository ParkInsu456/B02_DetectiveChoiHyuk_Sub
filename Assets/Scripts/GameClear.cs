using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameClear : MonoBehaviour
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

    // Ȱ��ȭ�� ������ �Է� ����
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

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
