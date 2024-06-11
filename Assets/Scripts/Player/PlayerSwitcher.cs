using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject[] players;  // 플레이어 오브젝트 배열
    public Transform[] spawnLocations;  // 각 플레이어의 스폰 위치 배열

    private int currentPlayerIndex = 0;

    private Vector3[] initialPositions; // 각 플레이어의 초기 위치 배열

    void Start()
    {
        // 초기 위치 저장
        initialPositions = new Vector3[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            initialPositions[i] = players[i].transform.position;
            players[i].SetActive(i == currentPlayerIndex);  // 초기에는 첫 번째 플레이어만 활성화
        }
    }

    public void SwitchPlayer(int playerIndex)
    {
        // 현재 활성화된 플레이어 비활성화
        players[currentPlayerIndex].SetActive(false);

        // 새로운 플레이어 인덱스로 전환
        currentPlayerIndex = playerIndex;

        // 새로운 플레이어를 초기 스폰 위치로 이동시키고 활성화
        players[currentPlayerIndex].transform.position = spawnLocations[currentPlayerIndex].position;
        players[currentPlayerIndex].SetActive(true);

        // Player_Modify를 제외한 나머지 플레이어의 위치 초기화
        for (int i = 1; i < players.Length; i++) // Player_Modify는 0번째 인덱스
        {
            if (i != currentPlayerIndex)
            {
                players[i].transform.position = initialPositions[i];
            }
        }
    }
}



