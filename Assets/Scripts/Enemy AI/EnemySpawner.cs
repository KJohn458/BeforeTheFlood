using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnHelpers;

    public static EnemySpawner Instance = null;

    Dictionary<GameObject, SpawnHelper> spawnDict = new Dictionary<GameObject, SpawnHelper>();

    private ObjectPooler.Key enemyKey = ObjectPooler.Key.Enemy;

    void Awake()
    {
        for (int i = 0; i < spawnHelpers.Length; i++)
        {
            spawnDict.Add(spawnHelpers[i], spawnHelpers[i].GetComponent<SpawnHelper>());
            spawnHelpers[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        Instance = null;
    }

    void Start()
    {
        EnableSpawnHelper();
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnEnemy();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            EnableSpawnHelper();
        }
    }

    public void SpawnEnemy()
    {
        List<GameObject> activeSpawnHelpers = ActiveSpawnHelpers(true);

        int rand = Random.Range(0, activeSpawnHelpers.Count);

        GameObject pooledObj = ObjectPooler.GetPooler(enemyKey).GetPooledObject();

        pooledObj.transform.position = spawnDict[activeSpawnHelpers[rand]].spawnPoint.position;

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
