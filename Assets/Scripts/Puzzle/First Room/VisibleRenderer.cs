using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class VisibleRenderer : MonoBehaviour
{
    public GameObject winePrefab;
    public MeshRenderer meshRenderer;
    public MeshRenderer[] meshs;
    private bool isOpen = false;

    // Update is called once per frame
    void Update()
    {

        if (isOpen)
        {
            return;
        }

        if (meshRenderer.isVisible)
        {
            ActivateObject(winePrefab, true);
            isOpen = true;
        }
    }

    public bool ActivateObject(GameObject obj, bool isActive)
    {
        foreach (MeshRenderer meshRenderer in meshs)
        {
            if (meshRenderer != null)
            {
                Debug.Log("mesh renderer ¿÷¿Ω");
                meshRenderer.enabled = true;
            }
            
            return true;
        }
        return isActive;
    }
}
