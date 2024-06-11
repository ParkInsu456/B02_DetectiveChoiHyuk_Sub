using UnityEngine;

//������ Ÿ��
public enum ItemType
{
    Usable,
    Consumable,
    Info
}

//����Ǵ� ���� Ÿ��
public enum ConsumableType
{
    Health,
    Speed
}

//�Ҹ� ������ ������
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; //����Ǵ� ���� Ÿ��
    public float value;         //����Ǵ� ��ġ [[ ex) type: Speed, value: 10.0f�̸� �Ҹ��� ��, �̵��ӵ��� 10.0f ���� ]]
}

//��ũ���ͺ������Ʈ
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    //������ ����
    [Header("Info")]
    public string ItemName;         //������ �̸�
    public string ItemDescription;  //������ ����
    public ItemType itemType;       //������ Ÿ�� 
    public Sprite ItemIcon;         //������ ������
    public GameObject ItemPrefab;   //������ ������
    public string ItemID;

    //���� �� �ִ� ������
    [Header("Stacking")]
    public bool CanStack;       //���� �� �ִ� ���������� Ȯ�ο� ����
    public int MaxStackAmount;  //����

    //�Ҹ� ������
    [Header("Consumable")]
    public ItemDataConsumable Consumable;   //�Ҹ� ������ ������

    [Header("Equip")]
    public GameObject equipPrefab;          //���� ���� ������
}