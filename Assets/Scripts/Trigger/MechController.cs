using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour, IInteractable, INumPadable
{
    // Mech컨트롤러에 캐싱되는 스크립트.//
    [SerializeField] private NumPad MechPanelUI; // 인스펙터 캐싱
    public Mech mech;
    public MechColor mechColor;

    public GameObject door;

    public string GetInteractPrompt()
    {
        string str = $"로봇 컨트롤러";
        return str ;
    }

    public void OnInteract()
    {
        // UI활성화, 마우스 활성화
        MechPanelUI.gameObject.SetActive(true);
        CharacterManager.Instance.Player.controller.ToggleCursor();

        // 컨트롤러에 상호작용하면 열리는 NumPad UI에 자신을 넣기
        MechPanelUI.numPadable = this;
    }

    // 효과음 발생 종료 후(몇초 기다리기) UI 닫기
    // 로봇행동 정지 ( 색점멸 중지, 로테이션 원래대로 돌리기)
    
    public void OnDone()
    {
        //mechColor.onBlinkEvent -= null;
        mechColor.StopBlinkColor();
        mech.DefaultRotation();
        MechPanelUI.CloseButton();
        // 로봇해제에 성공하면 문을 연다.
        if(door.TryGetComponent<ILock>(out ILock lockedDoor))
        {
            lockedDoor.Unlock();
        }
    }

    public void OnFail()
    {
    }
}

public interface INumPadable
{
    public void OnDone();
    public void OnFail();
}