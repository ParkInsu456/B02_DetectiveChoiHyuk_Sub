using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    public ItemData equipItem;
    public Action addItem;
    public Action OnEquipItem;
    public Action OffEquipItem;

    public Transform dropPosition;
    public GameObject equipCamera;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;   //캐릭터매니저에 플레이어에 나 자신을 집어넣어준다
        controller = GetComponent<PlayerController>();  //겟컴포넌트로 플레이어컨트롤러를 찾아와서 집어넣어줌
        condition = GetComponent<PlayerCondition>();
    }
}
