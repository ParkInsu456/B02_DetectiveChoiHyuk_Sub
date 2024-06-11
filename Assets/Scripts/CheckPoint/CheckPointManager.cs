using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public enum EnumCP
{
    First,
    Second,
    Third,
    Fourth
}

public class CheckPointManager : MonoBehaviour
{
    // 체크포인트를 담는 클래스

    public static CheckPointManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    // 가장 최신의 체크포인트를 저장하는 인스턴스
    static CPData cPData = new CPData();



    // 체크포인트를 활성화하는 기능
    // 새로운 층에 처음 올라가면 체크포인트 저장
    public List<CheckPoint> CheckPoints = new List<CheckPoint>();   // 인스펙터에서 넣기


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ExecuteLoad();
        }
    }



    public void SaveCheckPointData()
    {
        cPData.PlayerPosition = CharacterManager.Instance.Player.transform.position;
        cPData.PlayerQuaternion = CharacterManager.Instance.Player.transform.rotation;
        cPData.HealthCurValue = CharacterManager.Instance.Player.condition.health.CurValue;
        cPData.StaminaCurValue = CharacterManager.Instance.Player.condition.stamina.CurValue;
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(cPData);

        File.WriteAllText(Application.persistentDataPath + "/CheckPointData.txt", json);
        Debug.Log($"Saved : {Application.persistentDataPath}");
    }
    void Load()
    {
        var jsonData = File.ReadAllText(Application.persistentDataPath + "/CheckPointData.txt");
        cPData = JsonUtility.FromJson<CPData>(jsonData);
    }

    void LoadCheckPointData(CPData _CPData)
    {
        CharacterManager.Instance.Player.transform.position = _CPData.PlayerPosition;
        CharacterManager.Instance.Player.transform.rotation = _CPData.PlayerQuaternion;
        CharacterManager.Instance.Player.condition.health.CurValue = _CPData.HealthCurValue;
        CharacterManager.Instance.Player.condition.stamina.CurValue = _CPData.StaminaCurValue;
    }

    public void ExecuteLoad()
    {
        Load();
        LoadCheckPointData(cPData);
    }




}

[System.Serializable]
public class CPData
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerQuaternion;
    public float HealthCurValue;
    public float StaminaCurValue;
}