﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void SelectedChangeEvent(GameObject obj);
    public event SelectedChangeEvent ev;

    public static GameManager Instance = null;
    public int lives = 0;
    public bool lose = false;
    public int resource = 0;
    public int currentWave = 0;
    public bool planning = true;
    public float time = 0f;
    public float timeToNextWave = 45;
    public GameObject waveContainer;
    public bool win = false;
    public GameObject selected { get; private set; }
    public bool paused = false;
    public SwitchCanvas winC, loseC;

    private void OnDisable()
    {
        Instance = null;
        time = Time.timeSinceLevelLoad;
    }

    private void OnEnable()
    {
        Time.timeScale = 1f;
        time = Time.timeSinceLevelLoad;
        ObjectPoolManager.dict.Clear();
    }

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
        time = Time.timeSinceLevelLoad;
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
        time = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (!paused)
        {
            if (!win && !lose)
            {
                if (planning)
                {
                    if (Time.timeSinceLevelLoad - time >= timeToNextWave) BeginWave();
                }
                else
                {
                    Wave w = waveContainer.transform.GetChild(currentWave).GetComponent<Wave>();
                    if (w.done)
                    {
                        WaveComplete();
                    }
                    else
                    {
                        w.Spawn();
                    }
                }
            } else
            {
                HandleWinLose();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddLives(-500);
        }
    }

    public void HandleWinLose()
    {
        Time.timeScale = 0f;
        if (win) winC.Switch();
        else loseC.Switch();
    }

    public void ChangeSelection(GameObject o)
    {
        selected = o;
        ev?.Invoke(selected);
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
}
