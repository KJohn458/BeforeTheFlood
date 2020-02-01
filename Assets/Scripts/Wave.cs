using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemy;
    public int toSpawn;
    public int lane;
    public float spawnRate;
    [SerializeField]Queue<GameObject> enemies = new Queue<GameObject>();

    int spawned;
    int killed;
    float lastSpawned = 0f;
    public bool done { get { return (killed == spawned && spawned == toSpawn); } }

    private void Start()
    {
        CreateEnemy(toSpawn);
    }

    void CreateEnemy(int s = 1)
    {
        for (int i = 0; i < s; i++)
        {
            GameObject obj = Instantiate(enemy);
            obj.SetActive(false);
            obj.GetComponent<TestEnemy>().Create(this);
            enemies.Enqueue(obj);
        }
    }

    GameObject GetEnemyFromQueue()
    {
        if (enemies.Count == 0) CreateEnemy();
        return enemies.Dequeue();
    }

    public void Spawn()
    {
        bool childrenDone = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            Wave subwave = transform.GetChild(i).GetComponent<Wave>();
            if (subwave != null && !subwave.done)
            {
                childrenDone = false;
                subwave.Spawn();
            }
        }
        if (childrenDone && !done && spawned < toSpawn && lastSpawned + spawnRate < Time.time)
        {
            GameObject obj = GetEnemyFromQueue();
            obj.SetActive(true);
            //set enemy position
            spawned++;
            lastSpawned = Time.time;
        }
    }

    public void SpawnedEnemyKilled(GameObject e) {
        killed++;
        enemies.Enqueue(e);
    }
}
