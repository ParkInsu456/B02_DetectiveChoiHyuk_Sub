using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlane : MonoBehaviour
{
    public DollEvent dollEvent1;
    public DollEvent dollEvent2;

    private void Update()
    {
        if (dollEvent1.Doll && dollEvent2.Doll)
        {
            string str1 = dollEvent1.Doll.gameObject.name;
            string str2 = dollEvent2.Doll.gameObject.name;
            if (str1 == "Item_BlackDoll" && str2 == "Item_YellowDoll") Success();
        }
    }

    void Success()
    {
        Destroy(gameObject);
    }
}