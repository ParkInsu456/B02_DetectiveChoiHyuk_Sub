using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    // ���ӿ�����Ʈ Player�� ĳ�̵� Ŭ����
    // �÷��̾ ���� ��ġ�� ����. �����
    // ui�� �ٿ� ����Ǿ��ִ� value�� ������ ����.


    [SerializeField] private ConditionMgr conditionMgr; // ����Ƽ �ν����Ϳ��� ���� ĳ���ؾ���
    [SerializeField] private Gameover gameover;

    public Condition health
    {
        get { return conditionMgr.Health; }
    }
    public Condition stamina
    {
        get { return conditionMgr.Stamina; }
    }

    private event Action onTakeDamage;  // �÷��̾ �������� ���� �ð�ȿ���� �����ϴ� �׼�. 

    private bool isDie = false;

    void Update()
    {
        if (!isDie && health.CurValue <= 0f)
        {
            Die();
            isDie = true;
        }
    }

    private void Die()
    {
        // gameoverui Ȱ��ȭ
        gameover.ToggleUI();
    }


    public void TakeDamage(int damage)
    {
        health.MinusCurrent(damage);
        onTakeDamage?.Invoke();
    }

    public void IsDieFalse()
    {
        isDie = false;
    }
    
}
