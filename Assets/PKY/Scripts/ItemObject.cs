using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    private void Start()
    {
        //CharacterManager.Instance.Player.addItem += OnInteract;
    }

    public string GetInteractPrompt()
    {
        string str = $"{"E: ащ╠Б"}\n{data.ItemName}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
