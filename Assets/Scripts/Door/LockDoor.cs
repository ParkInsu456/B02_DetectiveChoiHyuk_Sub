using System.Collections;
using System.Collections.Generic;
using SojaExiles;
using UnityEngine;
using UnityEngine.InputSystem;



public class LockDoor : MonoBehaviour, IInteractable, ILock
{
    // 여닫히는 문을 잠그고 싶을때 문에 이 스크립트를 캐싱한다.

    public opencloseDoor door;
    public Animator animator;
    SojaExiles.opencloseDoor _opencloseDoor;
    AudioSource _audioSource;

    private bool isLock;

    private void Start()
    {
        animator = GetComponent<Animator>();
        door = GetComponent<opencloseDoor>();
        _opencloseDoor = GetComponent<SojaExiles.opencloseDoor>();
        _audioSource = GetComponent<AudioSource>();        
    }


    public void Lock()
    {
        if (isLock == true) return;
        isLock = true;
        if (door.open)
        {
            StartCoroutine(door.closing());
        }
        door.Player = null;
    }

    public void Unlock()
    {
        if (isLock == false) return;
        door.Player = CharacterManager.Instance.Player.transform;
        isLock = false;
    }


    // 잠긴 문을 상호작용시 잠겼다는 효과음과 문덜컹애니메이션 재생
    public void LockedDoorOpen()
    {
        StartCoroutine(TryOpen());
        _audioSource.Play();
    }
    IEnumerator TryOpen()
    {
        print("you are trying open locked door");
        _opencloseDoor.openandclose.Play("TryOpen");
        yield return new WaitForSeconds(.5f);
        _opencloseDoor.openandclose.Play("Idle");
    }

    public string GetInteractPrompt()
    {
        if (!isLock)
        {
            string strUnlock = "";
            return strUnlock;
        }    
        else
        {
            string str = $"이 문은 잠겨있다.";
            return str;
        }
    }

    public void OnInteract()
    {
        LockedDoorOpen();
    }
}
