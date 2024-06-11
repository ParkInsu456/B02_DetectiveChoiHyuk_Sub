using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour, IInteractable, ITrigger
{
    // 문이 열리면 트리거가 작동한다.
    public MechController mechController;   // 인스펙터캐싱
    SojaExiles.opencloseDoor _opencloseDoor;

    private bool isTrigger = false;

    private void Start()
    {
        _opencloseDoor = GetComponent<SojaExiles.opencloseDoor>();
    }

    private void Update()
    {
        if(_opencloseDoor.open && !isTrigger)
        {
            Execute();
            isTrigger = true;
        }
    }
    public void Execute()
    {
        mechController.mech.ToggleBoolisActive();
        mechController.mechColor.ToggleBoolisActive();
    }


    public void TriggerOn()
    {
        throw new System.NotImplementedException();
    }
    public string GetInteractPrompt()
    {
        string str = $"침실";
        return str;
    }

    public void OnInteract()
    {
        Execute();
    }
}
