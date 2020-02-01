using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    GameObject obj;
    Queue<GameObject> objs = new Queue<GameObject>();
    public ObjectPool(GameObject o)
    {
        obj = o;
    }

    public void Create(int s = 1)
    {
        int queueSize = objs.Count;
        for (int i = 0+queueSize; i < s; i++)
        {
            GameObject o = GameObject.Instantiate(obj);
            o.SetActive(false);
            objs.Enqueue(o);
        }
    }

    public GameObject Get()
    {
        if (objs.Count == 0) Create();
        return objs.Dequeue();
    }
    public void Return(GameObject obj)
    {
        objs.Enqueue(obj);
    }
}
