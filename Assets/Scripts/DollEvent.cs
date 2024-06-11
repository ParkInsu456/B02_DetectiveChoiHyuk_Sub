using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollEvent : MonoBehaviour
{
    public Transform Player;
    public UIInventory Inventory;

    public GameObject Doll;
    private bool isEmpty;

    void Start()
    {
        Player = CharacterManager.Instance.Player.transform;
        isEmpty = true;
        Doll = null;
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            Doll = null;
            isEmpty = true;
        }
    }

    void OnMouseOver()
    {
        if (Player)
        {
            float dist = Vector3.Distance(Player.position, transform.position);
            if (dist < 10)
            {
                if (CharacterManager.Instance.Player.equipItem && isEmpty)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        DisposeObject();
                    }
                }
            }
        }
    }

    void DisposeObject()
    {
        Doll = CharacterManager.Instance.Player.equipItem.ItemPrefab;
        Inventory.RemoveEquipItem();

        GameObject spawnedObject = Instantiate(Doll, transform);

        isEmpty = false;
    }
}