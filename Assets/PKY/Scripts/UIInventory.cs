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
    public ItemSlot[] slots;            //������ ���� �迭

    public GameObject inventoryWindow;  //�κ��丮 â
    public Transform slotPanel;         //���� �г�
    private Transform dropPosition;     //������ ������ ��ġ
    private GameObject equipCamera;     //���� ī�޶�
    //private GameObject equipItemObject; //����ִ� ������ ������Ʈ

    private ItemSlot selectedItem;                      //���õ� ������ ����
    private int selectedItemIndex;                      //���õ� ������ ������ ����

    [Header("SelectedItem")]
    public TextMeshProUGUI selectedItemName;            //���õ� ������ �̸� �ؽ�Ʈ
    public TextMeshProUGUI selectedItemDescription;     //���õ� ������ ���� �ؽ�Ʈ
    public GameObject useButton;                        //��� ��ư
    public GameObject equipButton;                      //���� ��ư
    public GameObject unEquipButton;                    //���� ���� ��ư
    public GameObject dropButton;                       //������ ��ư

    private int curEquipIndex;              //���� �������� ������ ����

    private PlayerController controller;    //�÷��̾� ��Ʈ�ѷ�
    private PlayerCondition condition;      //�÷��̾� �����

    void Start()
    {
        //�÷��̾� ����
        controller = CharacterManager.Instance.Player.controller;
        //condition = CharacterManager.Instance.Player.condition;

        dropPosition = CharacterManager.Instance.Player.dropPosition;
        equipCamera = CharacterManager.Instance.Player.equipCamera;

        //Toggle�� ActionInventory�� ��´�. ( �κ��丮�� �Ѵ� Ű�� ������ ��, Toggle�� ȣ���Ѵ� )
        controller.ActionInventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;
        //CharacterManager.Instance.Player.OnEquipItem += EquipItem;

        inventoryWindow.SetActive(false);           //�κ��丮 â ��Ȱ��ȭ
        slots = new ItemSlot[slotPanel.childCount]; //slots�ʱ�ȭ

        //���Ե��� �ʱ�ȭ�Ѵ�.
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;         //������ ������ �ִ´�
            slots[i].inventory = this;  //������ �κ��丮 ������ �� �κ��丮�� �����Ѵ�.
        }

        //�� �κ��丮�� �������� �ʱ�ȭ�Ѵ�. ( �ؽ�Ʈ�� ��ư )
        ClearSelectedItemWindow();
        UpdateUI();
    }

    void Update()
    {

    }

    //â ���� �ݴ� �Լ�
    public void Toggle()
    {
        //�κ��丮 â�� ���������� �ݰ�, ������ ������ ����.
        if (IsOpen()) inventoryWindow.SetActive(false);
        else inventoryWindow.SetActive(true);
    }

    //â�� ����� �ִ��� Ȯ�� �Լ�
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        //�������� ���̴� �������̸� ( ���� ���� ���� �� �ִ� ������ )
        if (data.CanStack)
        {
            //�κ��丮�� ���� �����۰� ���� �������� �ִ� ������ ã�´�
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                //���Կ� �ֿ� �������� �߰��Ѵ�.
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        //���� ������� �������� ������ ���� �ʴ� ��쿡�� ����ִ� ������ ��´�.
        ItemSlot emptySlot = GetEmptySlot();

        //����ִ� ������ ã�Ҵٸ�
        if (emptySlot != null)
        {
            emptySlot.item = data;      //���Կ� �������� �ִ´�
            emptySlot.quantity = 1;     //������ �ִ´�
            UpdateUI();                 //UI�� ������Ʈ�Ѵ�
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        //����ִ� ������ ã�� ���ߴٸ�
        ThrowItem(data);        //�տ� ��� �ִ� �������� ������.
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

    //UI�� ������Ʈ �����ִ� �Լ�
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

    //���� �� �ִ� ������ ������ ��ȯ�ϴ� �Լ� ( ������ �ִ� ������ )
    ItemSlot GetItemStack(ItemData data)
    {
        //������ ��� ��ȯ�Ѵ�
        for (int i = 0; i < slots.Length; i++)
        {
            //���Կ� �ִ� �����۰� ���� �ֿ��� �ϴ� �������� ���� �ִ� �������� �۴ٸ�
            if (slots[i].item == data && slots[i].quantity < data.MaxStackAmount)
            {
                //�� �������� ��ȯ�Ѵ�.
                return slots[i];
            }
        }
        return null;
    }

    //����ִ� ������ �־��ִ� �Լ�
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

    //�������� ������ ��, ȣ���ϴ� �Լ�
    public void SelectItem(int index)
    {
        //���Կ� �������� ������ �� �Լ��� �����Ѵ�
        if (slots[index].item == null) return;

        //���԰� ������ �ε����� ��´�
        selectedItem = slots[index];
        selectedItemIndex = index;

        //������ �̸��� ���� �ؽ�Ʈ�� ��´�
        selectedItemName.text = selectedItem.item.ItemName;
        selectedItemDescription.text = selectedItem.item.ItemDescription;

        useButton.SetActive(selectedItem.item.itemType == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.itemType == ItemType.Usable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.item.itemType == ItemType.Usable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    //�κ��丮 â�� �ִ� �ؽ�Ʈ�� ��ư Clear���ִ� �Լ�
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

    //��� ��ư ���� ��
    public void OnUseButton()
    {
        if (selectedItem.item.itemType == ItemType.Consumable)
        {
            switch (selectedItem.item.Consumable.type)
            {
                case ConsumableType.Health:
                    //ü�� ȸ�� �ڵ� �ۼ� ����
                    break;
                case ConsumableType.Speed:
                    //���ǵ� ��� �ڵ� �ۼ� ����
                    break;
            }

            RemoveSelctedItem();
        }
    }

    //������ ��ư ���� ��
    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelctedItem();
    }

    //���� ��ư ���� ��
    public void OnEquipButton()
    {
        //���� �����ϰ� �ִ� �������� ���´�
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;       //���� ������ �������� ������ true�� ����
        curEquipIndex = selectedItemIndex;              //���� �������� �������� ���� �ε����� ����
        CharacterManager.Instance.Player.equipItem = slots[selectedItemIndex].item; //�÷��̾ ������ ������ ����
        CharacterManager.Instance.Player.OnEquipItem?.Invoke();                     //�������� ������ �� ����� ��������Ʈ ȣ��
        UpdateUI();                                     //UI�� ������Ʈ�Ѵ�
        SelectItem(selectedItemIndex);                  //��ư�� �ʱ�ȭ�Ѵ�.( ���� ��ư�� ��Ȱ��ȭ�ǰ� ���� ���� ��ư�� Ȱ��ȭ�ȴ� )
    }

    //���� ���� ��ư ���� ��
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
                UnEquip(selectedItemIndex);   //���� ���� �Լ� ȣ��
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