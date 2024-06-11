using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 퍼즐 해결되면 재료 아이템 비활성화 되는 인터페이스 추가

// 오브젝트 활성화 시키면 해결되는 퍼즐
// 이 클래스를 상속받아서 구체적인 퍼즐 내용 작성
public class OnOffObjPuzzle : MonoBehaviour
{
    private Interaction interaction;
    
    // 자식 클래스에서 public GameObject의 형태로 프리팹을 할당할 변수 추가
    public virtual GameObject objectPrefab { get; protected set; }

    // 자식 클래스에서 private bool의 형태로 프리팹의 활성화 상태를 나타낼 변수 추가
    private bool objectActive = false;

    public GameObject keyPrefab;   // 열쇠 프리팹

    public virtual void Start()
    {
        interaction = FindObjectOfType<Interaction>();
        if (interaction == null)
        {
            Debug.Log("interaction null");
        }
    }

    public virtual void Update()   // 클릭 감지
    {
        if (interaction != null)
        {
            interaction.ClickPuzzleObj();
        }
    }

    // 클릭한 오브젝트, 놓인 오브젝트, 들고 있는 오브젝트 비교해서 모두 동일하면 해당 오브젝트 가시화
    public virtual void CheckAndActive(GameObject hitObject)
    {
        if (CharacterManager.Instance.Player.equipItem == null)
        {
            return;
        }

        GameObject curItem = CharacterManager.Instance.Player.equipItem.ItemPrefab;

        if (hitObject.name == curItem.name)
        {
            Debug.Log($"{hitObject.name} == {curItem.name}");
            if (hitObject.name == objectPrefab.name)
            {
                Debug.Log($"{hitObject.name} == {objectPrefab.name}");
                objectActive = ActivateObject(objectPrefab, objectActive);
            }
        }
    }

    public virtual bool ActivateObject(GameObject obj, bool isActive)  // 가시화 실제 수행 코드
    {
        MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            if (meshRenderer != null)
            {
                Debug.Log("mesh renderer 있음");
                meshRenderer.enabled = true;
            }
            CompletPuzzle();
            return true;
        }
        return isActive;
    }

    public virtual void CompletPuzzle() // 퍼즐 완성 조건 확인
    {
        Debug.Log("퍼즐 해결 조건 확인");
        if (objectActive)   // 오브젝트 개수 및 해결 조건에 따라 변경
        {
            Debug.Log("키 획득 조건 달성");
            CanGetKey();    // 열쇠 획득
        }
    }

    public void CanGetKey()     // 키 획득
    {
        Debug.Log("키 획득");
        if (keyPrefab.activeSelf)
        {
            return;
        }
        keyPrefab.SetActive(true);
    }
}
