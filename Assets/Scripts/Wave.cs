using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemy;
    public int toSpawn;
    public int lane;
    public float spawnRate;

    int spawned;
    int killed;
    float lastSpawned = 0f;
    bool done { get { return (killed == spawned && spawned == toSpawn); } }

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
        if (childrenDone && !done && lastSpawned + spawnRate < Time.time)
        {
            GameObject obj = Instantiate(enemy);
            //set enemy position
            spawned++;
            //hook up to enemy death
            lastSpawned = Time.time;
        }
    }

    public void SpawnedEnemyKilled() { killed++; }
}
