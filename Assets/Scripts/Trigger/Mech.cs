
using UnityEngine;

public class Mech : MonoBehaviour
{
    // Mech가 플레이어를 바라보게 하는 메서드.
    // Cockpit에 캐싱된다.
    public Player player;
    public bool isActive = false;

    private Quaternion defaultQuaternion;
    private bool isReturningToDefault = false;
    private float rotationSpeed = 1.0f; // 회전 속도


    private void Start()
    {
        player = CharacterManager.Instance.Player;
        defaultQuaternion = transform.rotation;
    }

    private void Update()
    {
        if(isActive)
        RotationCockpit();
        else if (isReturningToDefault)
        {
            ReturnToDefaultRotation();
        }
    }

    public void RotationCockpit()
    {        
        transform.LookAt(player.transform);
        transform.Rotate(0, 90, 0);
    }

    // 로테이션 원래대로 되돌리기
    public void DefaultRotation()
    {
        isActive = false;
        isReturningToDefault = true;
        // 로봇 꺼지는 효과음 재생
    }
    private void ReturnToDefaultRotation()
    {
        // Slerp를 사용하여 부드럽게 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, defaultQuaternion, Time.deltaTime * rotationSpeed);

        // 회전이 거의 완료되면 플래그를 끈다
        if (Quaternion.Angle(transform.rotation, defaultQuaternion) < 0.1f)
        {
            transform.rotation = defaultQuaternion;
            isReturningToDefault = false;
        }
    }

    public void ToggleBoolisActive()
    {
        isActive = !isActive;
    }
}
