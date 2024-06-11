using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    //상호작용할 아이템의 텍스트를 가져온다.
    public string GetInteractPrompt();
    //상호작용하는 함수이다.
    public void OnInteract();
}

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; //몇 초마다 체크할 것인지
    private float lastCheckTime;    //마지막에 체크한 시간
    public float maxCheckDistance;  //체크할 거리
    public LayerMask layerMask;     //레이어

    //캐싱할 자료들
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera _camera;
    private OnOffObjPuzzle onOffObjPuzzle;

    void Start()
    {
        //메인 카메라를 가져옴
        _camera = Camera.main;

        onOffObjPuzzle = FindObjectOfType<OnOffObjPuzzle>();
        if (onOffObjPuzzle == null)
        {
            Debug.Log("onOffObjPuzzle null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update문마다 Raycast하는 것을 방지, 0.05초 이상이 되었을 때 Raycast 가동
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            //카메라 정중앙
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            //레이캐스트에 성공할 시
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                //Layer가 Inertaction(7)이면 작동한다
                if (hit.collider.gameObject.layer == 7)
                {
                    // 태그가 PuzzleObject라면
                    if (hit.collider.gameObject.CompareTag("PuzzleObject"))
                    {
                        // 아무런 동작도 하지 않는다
                        // Debug.Log("Puzzle Object");
                    }
                    //이미 가지고 있는 정보가 아니라면
                    else if (hit.collider.gameObject != curInteractGameObject)
                    {
                        //정보를 가져온다
                        curInteractGameObject = hit.collider.gameObject;
                        curInteractable = hit.collider.GetComponent<IInteractable>();
                        SetPromptText();
                        Debug.Log("Ray Suc");
                    }
                }
                //Layer가 Inertaction(7)이 아니면 모든 아이템 정보를 비운다
                else
                {
                    curInteractGameObject = null;
                    curInteractable = null;
                    promptText.gameObject.SetActive(false);
                }
            }
            //레이캐스트에 실패할 시
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    //E키를 눌렀을 때
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        //아이템이 캐스팅됐을 때
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            //상호작용 한 후에 모두 비워주는 코드
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    // 퍼즐 오브젝트 클릭했을 때
    public void ClickPuzzleObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = CharacterManager.Instance.Player.controller.maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                Debug.Log($"{hit.collider.gameObject.name}");
                //onOffObjPuzzle = hit.collider.GetComponent<OnOffObjPuzzle>();
                if (onOffObjPuzzle != null)
                {
                    onOffObjPuzzle.CheckAndActive(hit.collider.gameObject);
                }
                else
                {
                    // Debug.Log("onOffObjPuzzle null");
                }
            }
        }
    }
}