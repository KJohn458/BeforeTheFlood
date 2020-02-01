using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    float timeStart;
    Wave source;

    public void Create(Wave s)
    {
        source = s;
    }

    public void OnEnable()
    {
        timeStart = Time.time;
    }

    void Update()
    {
        if (timeStart + 1.25f <= Time.time)
        {
            Die();
        }
    }

    void Die()
    {
        source.SpawnedEnemyKilled(gameObject);
        gameObject.SetActive(false);
    }
}
