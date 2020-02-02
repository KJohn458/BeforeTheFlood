using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    EnemyStateMachine stateMachine;
    NavMeshAgent nav;
    Health health;
    Camera camera;

    private Vector3 currentWaypoint = Vector3.zero;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        health = GetComponent<Health>();

        //Debug
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void OnEnable()
    {
        health.ResetHealth();
        health.OnDeath += OnEnemyDeath;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckDeathDebug();
        }
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

    public void CheckDeathDebug()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            if (objectHit.gameObject.tag == "Enemy")
            {
                health.TakeDamage(20);
            }
        }
    }

    void OnEnemyDeath()
    {
        health.OnDeath -= OnEnemyDeath;
        gameObject.SetActive(false);

        GameManager.Instance.killed++;
        Debug.Log("Enemies killed: " +  GameManager.Instance.killed);
    }

    void OnDisable()
    {
        currentWaypoint = Vector3.zero;
        stateMachine.switchState(EnemyStateMachine.StateType.Wait);
    }
}