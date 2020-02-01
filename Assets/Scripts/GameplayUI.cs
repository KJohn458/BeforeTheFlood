using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    public TextMeshProUGUI health, resources, wave, timer;

    void Update()
    {
        health.text = "Health: " + GameManager.Instance.lives;
        resources.text = "Resources: " + GameManager.Instance.resource;
        wave.text = "Wave " + (GameManager.Instance.currentWave+1);
        if (GameManager.Instance.planning && !GameManager.Instance.win)
        {
            wave.text = wave.text + " begins in:";
            timer.transform.parent.gameObject.SetActive(true);
            timer.text = (GameManager.Instance.timeToNextWave - (Time.time - GameManager.Instance.time)).ToString();
        } else
        {
            timer.transform.parent.gameObject.SetActive(false);
        }
    }
}
