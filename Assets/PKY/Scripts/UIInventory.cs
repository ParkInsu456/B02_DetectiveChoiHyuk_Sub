using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using Random = UnityEngine.Random;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;            //아이템 슬롯 배열

    public GameObject inventoryWindow;  //인벤토리 창
    public Transform slotPanel;         //슬롯 패널
    private Transform dropPosition;     //버려질 아이템 위치
    private GameObject equipCamera;     //장착 카메라
    //private GameObject equipItemObject; //들고있는 아이템 오브젝트

    private ItemSlot selectedItem;                      //선택된 아이템 슬롯
    private int selectedItemIndex;                      //선택된 아이템 슬롯의 순서

    [Header("SelectedItem")]
    public TextMeshProUGUI selectedItemName;            //선택된 아이템 이름 텍스트
    public TextMeshProUGUI selectedItemDescription;     //선택된 아이템 설명 텍스트
    public GameObject useButton;                        //사용 버튼
    public GameObject equipButton;                      //장착 버튼
    public GameObject unEquipButton;                    //장착 해제 버튼
    public GameObject dropButton;                       //버리기 버튼

    private int curEquipIndex;              //현재 장착중인 슬롯의 순서

    private PlayerController controller;    //플레이어 컨트롤러
    private PlayerCondition condition;      //플레이어 컨디션

    void Start()
    {
        //플레이어 관련
        controller = CharacterManager.Instance.Player.controller;
        //condition = CharacterManager.Instance.Player.condition;

        dropPosition = CharacterManager.Instance.Player.dropPosition;
        equipCamera = CharacterManager.Instance.Player.equipCamera;

        //Toggle을 ActionInventory에 담는다. ( 인벤토리를 켜는 키를 눌렀을 시, Toggle을 호출한다 )
        controller.ActionInventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;
        //CharacterManager.Instance.Player.OnEquipItem += EquipItem;

        inventoryWindow.SetActive(false);           //인벤토리 창 비활성화
        slots = new ItemSlot[slotPanel.childCount]; //slots초기화

        //슬롯들을 초기화한다.
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;         //슬롯의 순서를 넣는다
            slots[i].inventory = this;  //슬롯의 인벤토리 변수에 이 인벤토리로 설정한다.
        }

        //이 인벤토리의 설정들을 초기화한다. ( 텍스트나 버튼 )
        ClearSelectedItemWindow();
        UpdateUI();
    }

    void Update()
    {

    }

    //창 열고 닫는 함수
    public void Toggle()
    {
        //인벤토리 창이 열려있으면 닫고, 닫혀져 있으면 연다.
        if (IsOpen()) inventoryWindow.SetActive(false);
        else inventoryWindow.SetActive(true);
    }

    //창이 띄워져 있는지 확인 함수
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        //아이템이 쌓이는 아이템이면 ( 여러 개를 가질 수 있는 아이템 )
        if (data.CanStack)
        {
            //인벤토리에 지금 아이템과 같은 아이템이 있는 슬롯을 찾는다
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                //슬롯에 주운 아이템을 추가한다.
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        //만약 넣을라는 아이템을 가지고 있지 않는 경우에는 비어있는 슬롯을 얻는다.
        ItemSlot emptySlot = GetEmptySlot();

        //비어있는 슬롯을 찾았다면
        if (emptySlot != null)
        {
            emptySlot.item = data;      //슬롯에 아이템을 넣는다
            emptySlot.quantity = 1;     //수량을 넣는다
            UpdateUI();                 //UI를 업데이트한다
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        //비어있는 슬롯을 찾지 못했다면
        ThrowItem(data);        //손에 쥐고 있는 아이템을 버린다.
        CharacterManager.Instance.Player.itemData = null;
    }

    public void ThrowItem(ItemData data)
    {
        Instantiate(data.ItemPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    //public void EquipItem()
    //{
    //    var player = CharacterManager.Instance.Player;
    //    if (player.equipItem != null)
    //    {
    //        //equipItemObject = Instantiate(player.equipItem.equipPrefab, equipPosition.position, Quaternion.identity);
    //        //equipItemObject = Instantiate(player.equipItem.equipPrefab, equipCamera.transform);
    //    }
    //    else Debug.Log("player.equipItem == null");
    //}

    //UI를 업데이트 시켜주는 함수
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    //넣을 수 있는 아이템 슬롯을 반환하는 함수 ( 스택이 있는 아이템 )
    ItemSlot GetItemStack(ItemData data)
    {
        //슬롯을 모두 순환한다
        for (int i = 0; i < slots.Length; i++)
        {
            //슬롯에 있는 아이템과 지금 주울라고 하는 아이템이 같고 최대 수량보다 작다면
            if (slots[i].item == data && slots[i].quantity < data.MaxStackAmount)
            {
                //그 슬롯팡을 반환한다.
                return slots[i];
            }
        }
        return null;
    }

    //비어있는 슬롯을 넣어주는 함수
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    //아이템을 선택할 때, 호출하는 함수
    public void SelectItem(int index)
    {
        //슬롯에 아이템이 없으면 이 함수를 종료한다
        if (slots[index].item == null) return;

        //슬롯과 슬롯의 인덱스를 얻는다
        selectedItem = slots[index];
        selectedItemIndex = index;

        //아이템 이름과 설명 텍스트를 얻는다
        selectedItemName.text = selectedItem.item.ItemName;
        selectedItemDescription.text = selectedItem.item.ItemDescription;

        useButton.SetActive(selectedItem.item.itemType == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.itemType == ItemType.Usable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.item.itemType == ItemType.Usable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    //인벤토리 창에 있는 텍스트와 버튼 Clear해주는 함수
    void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    //사용 버튼 누를 시
    public void OnUseButton()
    {
        if (selectedItem.item.itemType == ItemType.Consumable)
        {
            switch (selectedItem.item.Consumable.type)
            {
                case ConsumableType.Health:
                    //체력 회복 코드 작성 예정
                    break;
                case ConsumableType.Speed:
                    //스피드 상승 코드 작성 예정
                    break;
            }

            RemoveSelctedItem();
        }
    }

    //버리기 버튼 누를 시
    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelctedItem();
    }

    //장착 버튼 누를 시
    public void OnEquipButton()
    {
        //지금 장착하고 있는 아이템을 벗는다
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;       //지금 슬롯의 아이템의 장착을 true로 설정
        curEquipIndex = selectedItemIndex;              //현재 장착중인 아이템의 슬롯 인덱스를 저장
        CharacterManager.Instance.Player.equipItem = slots[selectedItemIndex].item; //플레이어가 장착한 아이템 설정
        CharacterManager.Instance.Player.OnEquipItem?.Invoke();                     //아이템이 장착할 때 실행될 델리게이트 호출
        UpdateUI();                                     //UI를 업데이트한다
        SelectItem(selectedItemIndex);                  //버튼을 초기화한다.( 장착 버튼이 비활성화되고 장착 해제 버튼이 활성화된다 )
    }

    //장착 해제 버튼 누를 시
    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    void RemoveSelctedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            if (slots[selectedItemIndex].equipped)
            {
                UnEquip(selectedItemIndex);   //장착 해제 함수 호출
            }

            selectedItem.item = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        //Destroy(equipItemObject.gameObject);
        //equipItemObject = null;
        CharacterManager.Instance.Player.equipItem = null;
        CharacterManager.Instance.Player.OffEquipItem?.Invoke();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }

    public void RemoveEquipItem()
    {
        UnEquip(curEquipIndex);
        if (selectedItemIndex == curEquipIndex)
        {
            selectedItem.item = null;
            slots[curEquipIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
            UpdateUI();
        }
    }
}