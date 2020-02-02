using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnHelpers;

    Dictionary<GameObject, SpawnHelper> spawnDict = new Dictionary<GameObject, SpawnHelper>();

    private ObjectPooler.Key enemyKey = ObjectPooler.Key.Enemy;

    public float spawnBuffer;

    public float waveLaneIncrease;

    private bool hasWaveStarted = false;

    void Awake()
    {
        for (int i = 0; i < spawnHelpers.Length; i++)
        {
            spawnDict.Add(spawnHelpers[i], spawnHelpers[i].GetComponent<SpawnHelper>());
            spawnHelpers[i].SetActive(false);
        }
    }

    void Start()
    {
        EnableSpawnHelper();
    }

    void Update()
    {
        CheckForWaveStart();

        if (hasWaveStarted)
        {
            if (Time.time - spawnBuffer >= GameManager.Instance.timeToNextSpawn && GameManager.Instance.spawned < GameManager.Instance.fib[0])
            {
                SpawnEnemy();
                GameManager.Instance.spawned++;
                spawnBuffer = Time.time;
            }

            if (GameManager.Instance.spawned == GameManager.Instance.fib[0] && GameManager.Instance.killed == GameManager.Instance.fib[0])
            {
                hasWaveStarted = false;
            }
        }
    }

    void CheckForWaveStart()
    {

        if ((int)(GameManager.Instance.timeToNextWave - (Time.timeSinceLevelLoad - GameManager.Instance.time)) <= 0 && !hasWaveStarted)
        {
            spawnBuffer = Time.time;

            if ((GameManager.Instance.currentWave + 1) % waveLaneIncrease == 0)
            {
                EnableSpawnHelper();
            }

            hasWaveStarted = true;
        }
    }

    public void SpawnEnemy()
    {
        List<GameObject> activeSpawnHelpers = ActiveSpawnHelpers(true);

        int rand = Random.Range(0, activeSpawnHelpers.Count);

        GameObject pooledObj = ObjectPooler.GetPooler(enemyKey).GetPooledObject();

        pooledObj.transform.position = spawnDict[activeSpawnHelpers[rand]].spawnPoints[GameManager.Instance.currentWaterLevel].position;

        Quaternion targetRotation = Quaternion.LookRotation(spawnDict[activeSpawnHelpers[rand]].wayPoint.position);
        pooledObj.transform.rotation = targetRotation;

        pooledObj.SetActive(true);
        pooledObj.GetComponent<EnemyBehavior>().FindPath(spawnDict[activeSpawnHelpers[rand]].wayPoint);
    }

    void EnableSpawnHelper()
    {
        List<GameObject> tempSpawnHelpers = ActiveSpawnHelpers(false);

        int rand = Random.Range(0, tempSpawnHelpers.Count);
        if (tempSpawnHelpers.Count != 0)
        {
            tempSpawnHelpers[rand].SetActive(true);
        }
        return;
    }

   List<GameObject> ActiveSpawnHelpers(bool checkingActive)
    {
        List<GameObject> tempSpawnHelpers = new List<GameObject>();

        for (int i = 0; i < spawnHelpers.Length; i++)
        {
            if (!spawnHelpers[i].gameObject.activeInHierarchy && !checkingActive)
            {
                tempSpawnHelpers.Add(spawnHelpers[i]);
            }
            else if (spawnHelpers[i].gameObject.activeInHierarchy && checkingActive)
            {
                tempSpawnHelpers.Add(spawnHelpers[i]);
            }
        }

        return tempSpawnHelpers;
    }
}
