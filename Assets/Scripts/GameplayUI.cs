﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    public TextMeshProUGUI health, resources, wave, timer;
    public GameObject upgradePopup;

    private void Start()
    {
        GameManager.Instance.ev += SelectionChanged;
    }

    void Update()
    {
        health.text = "Health: " + GameManager.Instance.lives;
        resources.text = "Resources: " + GameManager.Instance.resource;
        wave.text = "Wave " + (GameManager.Instance.currentWave+1);
        if (GameManager.Instance.planning && !GameManager.Instance.win)
        {
            wave.text = wave.text + " begins in:";
            timer.transform.parent.gameObject.SetActive(true);
            timer.text = ((int)(GameManager.Instance.timeToNextWave - (Time.time - GameManager.Instance.time))+1).ToString();
        } else
        {
            timer.transform.parent.gameObject.SetActive(false);
        }
    }

    void SelectionChanged(GameObject obj)
    {
        if (obj == null) upgradePopup.SetActive(false);
        else upgradePopup.SetActive(true);
    }
}