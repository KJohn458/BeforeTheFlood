using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static Dictionary<GameObject, ObjectPool> dict = new Dictionary<GameObject, ObjectPool>();

    public static ObjectPool getPool(GameObject obj)
    {
        ObjectPool o;
        if (!dict.TryGetValue(obj, out o))
        {
            ObjectPool n = new ObjectPool(obj);
            dict.Add(obj, n);
            return n;
        }
        return o;
    }
}
