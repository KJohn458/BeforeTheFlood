﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackTurret : MonoBehaviour
{
    [SerializeField]
    private BasicEnemy enemy;
    //private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        //manager.getTimer(); or somethin like that
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemy = other.GetComponent<BasicEnemy>();
            enemy.subHealth();
            
            Debug.Log("Fire");
        }
    }
}
