using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class FourDuck : OnOffObjPuzzle
{
    // 식탁 위의 오리 오브젝트
    public GameObject tableDuckFront;
    public GameObject tableDuckBack;
    public GameObject tableDuckLeft;
    public GameObject tableDuckRight;

    // 활성화 상태
    private bool frontActive = false;
    private bool backActive = false;
    private bool leftActive = false;
    private bool rightActive = false;

    // 클릭한 오브젝트, 놓인 오브젝트, 들고 있는 오브젝트 비교해서 모두 동일하면 해당 오브젝트 가시화
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

    public override void CompletPuzzle() // 퍼즐 완성 조건 확인
    {
        Debug.Log("퍼즐 해결 조건 확인");

        if (frontActive && backActive && leftActive && rightActive)
        {
            Debug.Log("키 획득 조건 달성");
            CanGetKey();    // 열쇠 획득
        }
    }
}
