using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class CheckPoint : MonoBehaviour
{
    // üũ����Ʈ�� �� ��ũ��Ʈ. 
    // �ѹ� �����ϸ� �ٽ� ������� �ʴ´�.
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
