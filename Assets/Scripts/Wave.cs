using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemy;
    public int toSpawn;
    public int lane;

    int spawned;
    int killed;
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
        if (childrenDone && !done)
        {
            GameObject obj = Instantiate(enemy);
            //set enemy position
            spawned++;
            //hook up to enemy death
        }
    }

    public void SpawnedEnemyKilled() { killed++; }
}
