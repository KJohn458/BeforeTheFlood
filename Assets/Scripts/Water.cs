using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Water : MonoBehaviour
{
    public static Water Instance;
    int currentHeight = -1;
    public float[] heights;
    public int[] costs;
    public bool finished { get { return (currentHeight >= heights.Length - 1); } }

    private void Start()
    {
        Instance = this;


    }

    public int GetCost() { return costs[currentHeight + 1]; }

    public void Buy()
    {
        if (!finished) {
            if (GameManager.Instance.SpendResource(costs[currentHeight+1]))
            {
                currentHeight++;
                transform.DOMove(new Vector3(transform.position.x, heights[currentHeight], transform.position.z), 3);

                GameManager.Instance.currentWaterLevel++;
            }
        }
    }
}
