using UnityEngine;

//아이템 타입
public enum ItemType
{
    Usable,
    Consumable,
    Info
}

//적용되는 스탯 타입
public enum ConsumableType
{
    Health,
    Speed
}

//소모 아이템 데이터
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; //적용되는 스탯 타입
    public float value;         //적용되는 수치 [[ ex) type: Speed, value: 10.0f이면 소모할 시, 이동속도가 10.0f 증가 ]]
}

//스크립터블오브젝트
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    //아이템 정보
    [Header("Info")]
    public string ItemName;         //아이템 이름
    public string ItemDescription;  //아이템 설명
    public ItemType itemType;       //아이템 타입 
    public Sprite ItemIcon;         //아이템 아이콘
    public GameObject ItemPrefab;   //아이템 프리팹
    public string ItemID;

    //쌓일 수 있는 아이템
    [Header("Stacking")]
    public bool CanStack;       //쌓일 수 있는 아이템인지 확인용 변수
    public int MaxStackAmount;  //수량

    //소모 아이템
    [Header("Consumable")]
    public ItemDataConsumable Consumable;   //소모 아이템 데이터

    [Header("Equip")]
    public GameObject equipPrefab;          //장착 중인 아이템
}