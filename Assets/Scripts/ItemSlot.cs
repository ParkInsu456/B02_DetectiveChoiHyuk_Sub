using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;               //�� ���Կ� �־��� �ִ� ������
    public UIInventory inventory;       //�κ��丮â
    public Button button;               //������ ��ư
    public Image icon;                  //������
    public TextMeshProUGUI quatityText; //���� �ؽ�Ʈ
    [SerializeField] private Outline outline;            //�׵θ� ��

    public int index;       //�� ������ ������ ����
    public bool equipped;   //������ ���������� Ȯ�ο�
    public int quantity;    //����

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //������ �����ϴ� �Լ�
    public void Set()
    {
        icon.gameObject.SetActive(true);    //������ Ȱ��ȭ�Ѵ�
        icon.sprite = item.ItemIcon;        //������ �̹����� �ִ´�
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;   //������ 2���� �۴ٸ� �� �ؽ�Ʈ �ƴϸ� ������ �ִ´�

        if (outline != null)
        {
            outline.enabled = equipped;     //�����ϸ� �׵θ��� Ȱ��ȭ�Ѵ�
        }
    }

    //������ ����ִ� �Լ�
    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
        outline.enabled = equipped;     //�׵θ��� ��Ȱ��ȭ�Ѵ�
    }

    //������ Ŭ���� ��, ȣ���ϴ� �Լ�
    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }


}
