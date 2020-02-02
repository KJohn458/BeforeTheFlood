using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    EnemyStateMachine stateMachine;
    NavMeshAgent nav;
    Health health;

    public int resourceGained;

    private Vector3 currentWaypoint = Vector3.zero;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        health = GetComponent<Health>();

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
        if (Vector3.Distance(transform.position, currentWaypoint) < 2f)
        {
            stateMachine.switchState(EnemyStateMachine.StateType.Attack);
        }
    }

    public void Attacking()
    {
        Debug.Log("Attacking");
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