using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;               //이 슬롯에 넣어져 있는 아이템
    public UIInventory inventory;       //인벤토리창
    public Button button;               //슬롯의 버튼
    public Image icon;                  //아이콘
    public TextMeshProUGUI quatityText; //수량 텍스트
    [SerializeField] private Outline outline;            //테두리 선

    public int index;       //이 아이템 슬롯의 순서
    public bool equipped;   //장착된 아이템인지 확인용
    public int quantity;    //수량

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //슬롯을 세팅하는 함수
    public void Set()
    {
        icon.gameObject.SetActive(true);    //아이콘 활성화한다
        icon.sprite = item.ItemIcon;        //아이템 이미지를 넣는다
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;   //수량이 2보다 작다면 빈 텍스트 아니면 수량을 넣는다

        if (outline != null)
        {
            outline.enabled = equipped;     //장착하면 테두리를 활성화한다
        }
    }

    //슬롯을 비워주는 함수
    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
        outline.enabled = equipped;     //테두리를 비활성화한다
    }

    //슬롯을 클릭할 때, 호출하는 함수
    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }


}
