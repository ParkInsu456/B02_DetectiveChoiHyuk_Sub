using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: ���� �ذ�Ǹ� ��� ������ ��Ȱ��ȭ �Ǵ� �������̽� �߰�

// ������Ʈ Ȱ��ȭ ��Ű�� �ذ�Ǵ� ����
// �� Ŭ������ ��ӹ޾Ƽ� ��ü���� ���� ���� �ۼ�
public class OnOffObjPuzzle : MonoBehaviour
{
    private Interaction interaction;
    
    // �ڽ� Ŭ�������� public GameObject�� ���·� �������� �Ҵ��� ���� �߰�
    public virtual GameObject objectPrefab { get; protected set; }

    // �ڽ� Ŭ�������� private bool�� ���·� �������� Ȱ��ȭ ���¸� ��Ÿ�� ���� �߰�
    private bool objectActive = false;

    public GameObject keyPrefab;   // ���� ������

    public virtual void Start()
    {
        interaction = FindObjectOfType<Interaction>();
        if (interaction == null)
        {
            Debug.Log("interaction null");
        }
    }

    public virtual void Update()   // Ŭ�� ����
    {
        if (interaction != null)
        {
            interaction.ClickPuzzleObj();
        }
    }

    // Ŭ���� ������Ʈ, ���� ������Ʈ, ��� �ִ� ������Ʈ ���ؼ� ��� �����ϸ� �ش� ������Ʈ ����ȭ
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

    public virtual bool ActivateObject(GameObject obj, bool isActive)  // ����ȭ ���� ���� �ڵ�
    {
        MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            if (meshRenderer != null)
            {
                Debug.Log("mesh renderer ����");
                meshRenderer.enabled = true;
            }
            CompletPuzzle();
            return true;
        }
        return isActive;
    }

    public virtual void CompletPuzzle() // ���� �ϼ� ���� Ȯ��
    {
        Debug.Log("���� �ذ� ���� Ȯ��");
        if (objectActive)   // ������Ʈ ���� �� �ذ� ���ǿ� ���� ����
        {
            Debug.Log("Ű ȹ�� ���� �޼�");
            CanGetKey();    // ���� ȹ��
        }
    }

    public void CanGetKey()     // Ű ȹ��
    {
        Debug.Log("Ű ȹ��");
        if (keyPrefab.activeSelf)
        {
            return;
        }
        keyPrefab.SetActive(true);
    }
}
