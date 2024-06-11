using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public PlayerSwitcher playerSwitcher;
    public int playerIndex;  // 전환할 플레이어 인덱스

    void OnMouseDown()
    {
        playerSwitcher.SwitchPlayer(playerIndex);
    }
}



