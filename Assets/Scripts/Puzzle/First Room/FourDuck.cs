using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class FourDuck : OnOffObjPuzzle
{
    // ��Ź ���� ���� ������Ʈ
    public GameObject tableDuckFront;
    public GameObject tableDuckBack;
    public GameObject tableDuckLeft;
    public GameObject tableDuckRight;

    // Ȱ��ȭ ����
    private bool frontActive = false;
    private bool backActive = false;
    private bool leftActive = false;
    private bool rightActive = false;

    // Ŭ���� ������Ʈ, ���� ������Ʈ, ��� �ִ� ������Ʈ ���ؼ� ��� �����ϸ� �ش� ������Ʈ ����ȭ
    public override void CheckAndActive(GameObject hitObject)
    {
        if (CharacterManager.Instance.Player.equipItem == null)
        {
            Debug.Log("No item equiped");
            return;
        }

        GameObject curItem = CharacterManager.Instance.Player.equipItem.ItemPrefab;

        if (hitObject.name == curItem.name)
        {
            Debug.Log($"{hitObject.name} == {curItem.name}");    
            if (hitObject.name == tableDuckFront.name)    
            {
                Debug.Log($"{hitObject.name} == {tableDuckFront.name}");
                frontActive = ActivateObject(tableDuckFront, frontActive);
            }
            else if (hitObject == tableDuckBack)
            {
                backActive = ActivateObject(tableDuckBack, backActive);
            }
            else if (hitObject == tableDuckLeft)
            {
                leftActive = ActivateObject(tableDuckLeft, leftActive);
            }
            else if (hitObject == tableDuckRight)
            {
                rightActive = ActivateObject(tableDuckRight, rightActive);
            }
        }
    }

    public override void CompletPuzzle() // ���� �ϼ� ���� Ȯ��
    {
        Debug.Log("���� �ذ� ���� Ȯ��");

        if (frontActive && backActive && leftActive && rightActive)
        {
            Debug.Log("Ű ȹ�� ���� �޼�");
            CanGetKey();    // ���� ȹ��
        }
    }
}
