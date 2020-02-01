using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    public GameObject tgt;
    public bool togglePause = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Switch();
        }
    }

    public void Switch()
    {
        tgt.SetActive(true);
        gameObject.SetActive(false);
        if (togglePause) GameManager.Instance.TogglePause();
    }
}
