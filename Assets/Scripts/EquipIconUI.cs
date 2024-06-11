using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipIconUI : MonoBehaviour
{
    private Image icon;                  //æ∆¿Ãƒ‹

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    private void Start()
    {
        CharacterManager.Instance.Player.OnEquipItem += Set;
        CharacterManager.Instance.Player.OffEquipItem += Clear;
    }

    void Set()
    {
        icon.sprite = CharacterManager.Instance.Player.equipItem.ItemIcon;
    }

    void Clear()
    {
        icon.sprite = null;
    }
}
