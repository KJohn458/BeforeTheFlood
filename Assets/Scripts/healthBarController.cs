using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarController : MonoBehaviour
{
    private Slider healthBar;
        
    void Start()
    {
        healthBar = GetComponentInChildren<Slider>();
       
    }

    void Update()
    {
       healthBar.value = GameManager.Instance.lives;
    }
    
}
