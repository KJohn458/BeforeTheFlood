using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    public GameObject tgt;
    public GameObject src = null;
    public bool togglePause = true;

    private void Update()
    {
        if (togglePause && !GameManager.Instance.win && !GameManager.Instance.lose)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Switch();
            }
        }
    }

    public void Switch()
    {
        tgt.SetActive(true);
        if (src != null) src.SetActive(false); else gameObject.SetActive(false);
        if (togglePause) GameManager.Instance.TogglePause();
    }
}
