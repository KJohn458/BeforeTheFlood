using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemy;
    public int toSpawn;
    public int lane;
    public float spawnRate;
    Queue<GameObject> enemies = new Queue<GameObject>();

    int spawned;
    int killed;
    float lastSpawned = 0f;
    public bool done { get { return (toSpawn == 0) ? ChildrenDone() : (killed == spawned && spawned == toSpawn); } }
    public bool handleChildrenAllAtOnce = false;
    int currentWave = 0;

    ObjectPool p;

    private void Start()
    {
        p = ObjectPoolManager.getPool(enemy);
        p.Create(toSpawn);
    }

    public bool ChildrenDone()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Wave subwave = transform.GetChild(i).GetComponent<Wave>();
            if (subwave != null && !subwave.done)
            {
                return false;
            }
        }
        return true;
    }

    public void Spawn()
    {
        bool childrenDone = true;
        if (handleChildrenAllAtOnce)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Wave subwave = transform.GetChild(i).GetComponent<Wave>();
                if (subwave != null && !subwave.done)
                {
                    childrenDone = false;
                    subwave.Spawn();
                }
            }
        } else
        {
            childrenDone = (currentWave == transform.childCount);
            if (!childrenDone)
            {
                Wave subwave = transform.GetChild(currentWave).GetComponent<Wave>();
                if (subwave.done) currentWave++;
                else subwave.Spawn();
            }
        }
        if (childrenDone && !done && spawned < toSpawn && lastSpawned + spawnRate < Time.time)
        {
            GameObject obj = p.Get();
            obj.SetActive(true);
            obj.GetComponent<TestEnemy>().Create(this);
            //set enemy position
            spawned++;
            lastSpawned = Time.time;
        }
    }

    public void SpawnedEnemyKilled(GameObject e) {
        killed++;
        p.Return(e);
    }
}
