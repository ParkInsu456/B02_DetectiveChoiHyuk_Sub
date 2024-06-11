using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutWine : OnOffObjPuzzle
{
    [SerializeField]
    private GameObject winePrefab;

    public override void Start()
    {
        base.Start();
        // winePrefab = GetComponent<GameObject>();
        if (winePrefab != null)
        {
            objectPrefab = winePrefab;
        }
        else
        {
            Debug.Log("winePrifab null");
        }
    }

    public override void CheckAndActive(GameObject hitObject)
    {
        Debug.Log("PutWine Logic");
        base.CheckAndActive(hitObject);
    }
}