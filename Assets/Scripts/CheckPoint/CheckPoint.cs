using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class CheckPoint : MonoBehaviour
{
    // 체크포인트에 들어갈 스크립트. 
    // 한번 접촉하면 다시 저장되지 않는다.
    private bool isChecked = false;

        

    private void OnTriggerEnter(Collider other)
    {
        if(!isChecked && other.gameObject.CompareTag("Player"))
        {
            CheckPointManager.instance.SaveCheckPointData();
            CheckPointManager.instance.Save();
            isChecked = true;
        }
    }
}
