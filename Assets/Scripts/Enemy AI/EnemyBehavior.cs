using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyBehavior : MonoBehaviour
{
    EnemyStateMachine stateMachine;
    NavMeshAgent nav;
    Health health;

    public int resourceGained;

    private Vector3 currentWaypoint = Vector3.zero;

    private float attackTimer;
    public float attackBuffer;
    public GameObject attackBar;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        health = GetComponent<Health>();

    }

    void Start()
    {
        attackTimer = Time.time;
    }

    void OnEnable()
    {
        health.ResetHealth();
        health.OnDeath += OnEnemyDeath;
    }

    public void FindPath(Transform waypoint)
    {
        currentWaypoint = waypoint.position;
        nav.SetDestination(waypoint.position);
        stateMachine.switchState(EnemyStateMachine.StateType.Walk);
    }

    public void Walking()
    {
        if (Vector3.Distance(transform.position, currentWaypoint) < 10f)
        {
            stateMachine.switchState(EnemyStateMachine.StateType.Attack);
        }
    }

    public void Attacking()
    {

        if (Time.time - attackTimer >= attackBuffer)
        {
            attackBar.transform.DOPunchScale(new Vector3(.1f, .1f, .8f), 1, 0, 0);

            GameManager.Instance.AddLives(-1);

            attackTimer = Time.time;
        }
    }

    void OnEnemyDeath()
    {
        health.OnDeath -= OnEnemyDeath;
        gameObject.SetActive(false);

        GameManager.Instance.killed++;
        GameManager.Instance.AddResource(resourceGained);
    }

    void OnDisable()
    {
        currentWaypoint = Vector3.zero;
        stateMachine.switchState(EnemyStateMachine.StateType.Wait);
    }
}