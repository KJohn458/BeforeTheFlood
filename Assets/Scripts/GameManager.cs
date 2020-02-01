using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int lives = 0;
    public bool lose = false;
    public int resource = 0;
    public int currentWave = 0;
    public bool planning = true;
    float time = 0f;
    public float timeToNextWave = 45;
    public GameObject waveContainer;
    public bool win = false;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void AddLives(int count)
    {
        lives += count;
        if (lives < 1) lose = true;
    }

    public void AddResource(int count) { resource += count; }
    public bool SpendResource(int cost)
    {
        if (resource >= cost)
        {
            resource -= cost;
            return true;
        }
        return false;
    }

    public void WaveComplete()
    {
        time = Time.time;
        planning = true;
        currentWave++;
        if (currentWave == waveContainer.transform.childCount)
        {
            win = true;
        }
    }

    public void BeginWave()
    {
        planning = false;
        time = Time.time;
    }

    private void Update()
    {
        if (!win && !lose)
        {
            if (planning)
            {
                if (Time.time - time >= timeToNextWave) BeginWave();
            }
            else
            {
                Wave w = waveContainer.transform.GetChild(currentWave).GetComponent<Wave>();
                if (w.done)
                {
                    WaveComplete();
                } else
                {
                    w.Spawn();
                }
            }
        }
    }
}
