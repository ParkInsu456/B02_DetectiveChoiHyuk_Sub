using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockTrigger : MonoBehaviour, ITrigger
{
    // I

    public GameObject eventObject; // 잠그고자 하는 문을 인스펙터에서 직접 캐싱
    public ILock ilock;
  

    
    private bool isTriggerOn = false;

    private void Start()
    {
        if (eventObject.TryGetComponent<ILock>(out ILock _ilock))
        {
            ilock = _ilock;
        }
        
    }

    public void Execute()
    {
        ilock.Lock();
    }

    public void TriggerOn()
    {
        isTriggerOn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggerOn)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TriggerOn();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isTriggerOn)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Execute();
                gameObject.SetActive(false);
            }
        }
    }

}
