using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    public GameObject levels;
    int current = 0;
    public GameObject left, right;
    public TextMeshProUGUI levelname;
    GameObject currentLvl;

    private void Start()
    {
        ChangeLevel(0);
    }

    public void ChangeLevel(int val)
    {
        current += val;
        currentLvl?.SetActive(false);
        currentLvl = levels.transform.GetChild(current).gameObject;
        currentLvl?.SetActive(true);
        levelname.text = levels.transform.GetChild(current).name;
        left.SetActive(current > 0);
        right.SetActive(current < levels.transform.childCount-1);
    }
    public void Switch()
    {
        SceneManager.LoadScene(currentLvl.GetComponent<Level>().levelSceneName);
    }
}
