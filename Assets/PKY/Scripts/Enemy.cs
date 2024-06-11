using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AIState
{
    Idle,
    Running
}

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float runSpeed;

    [Header("AI")]
    private AIState aiState;

    //private float playerDistance;

    private NavMeshAgent agent;
    private Animator animator;

    private CapsuleCollider capsuleCollider;

    private AudioSource audioSource; // 소리를 재생할 AudioSource

    [SerializeField] GameObject gameOver;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        SetState(AIState.Idle);
    }

    private void Update()
    {
        //playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        animator.SetBool("IsRunning", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle:
                break;
            case AIState.Running:
                ChasePlayer();
                break;
        }
    }

    public void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                //agent.speed = runSpeed;
                agent.isStopped = true;
                break;
            case AIState.Running:
                agent.speed = runSpeed;
                audioSource.Play();
                agent.isStopped = false;
                break;
        }
    }

    private void ChasePlayer()
    {
        Transform playerTransform = CharacterManager.Instance.Player.transform;
        //transform.LookAt(playerTransform);
        //transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, runSpeed * Time.deltaTime);

        // 멈춘다
        //agent.isStopped = true;

        // 플레이어를 따라간다
        agent.isStopped = false;
        agent.SetDestination(playerTransform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            //사망
            Debug.Log("사망");
            gameOver.SetActive(true);
        }
    }
}
