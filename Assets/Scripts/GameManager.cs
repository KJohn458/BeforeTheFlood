using System.Collections;
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
    public float timeToNextSpawn = .8f;
    public GameObject waveContainer;
    public bool win = false;
    public GameObject selected { get; private set; }
    public bool paused = false;
    public SwitchCanvas winC, loseC;

    public int[] fib = { 3, 5 };
    public int spawned;
    public int killed;

    public int currentWaterLevel = 0;

    public Camera mainCamera;

    private void OnDisable()
    {
        Instance = null;
        time = Time.timeSinceLevelLoad;
    }

    public void SpawnedEnemyKilled()
    {
        killed++;
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

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        int i = fib[0] + fib[1];
        fib[0] = fib[1];
        fib[1] = i;
        spawned = 0;
        killed = 0;
        currentWave++;
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
                    if (spawned == fib[0] && killed == fib[0])
                    {
                        WaveComplete();
                    } else if (Time.timeSinceLevelLoad - time >= timeToNextSpawn)
                    {
                        //EnemySpawner.Instance.SpawnEnemy();
                       // time = Time.timeSinceLevelLoad;
                        //spawned++;
                        //killed++;
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
