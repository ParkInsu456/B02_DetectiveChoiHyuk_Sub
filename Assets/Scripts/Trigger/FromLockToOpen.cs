using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromLockToOpen : MonoBehaviour
{
    public GameObject lookDoor;

    private void Update()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = CharacterManager.Instance.Player.controller.maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (CharacterManager.Instance.Player.equipItem == null)
            {
                Debug.Log("No item equiped");
                return;
            }

            GameObject curItem = CharacterManager.Instance.Player.equipItem.ItemPrefab;

            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                Debug.Log("레이캐스트 됨");
                if (hit.collider.gameObject == lookDoor && curItem.CompareTag("Key"))
                {
                    Debug.Log("첫 번째 조건 통과");
                    SojaExiles.opencloseDoor doorScript = lookDoor.GetComponent<SojaExiles.opencloseDoor>();
                    if (doorScript != null)
                    {
                        Debug.Log("두 번째 조건 통과");
                        doorScript.enabled = true; // 스크립트 활성화
                        doorScript.Player = CharacterManager.Instance.Player.transform;
                        Debug.Log("활성화 완료");
                    }
                }
            }
        }
    }
}
