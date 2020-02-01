﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string levelName;
    public void Switch()
    {
        SceneManager.LoadScene(levelName);
    }
}
