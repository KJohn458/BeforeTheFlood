﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurret : MonoBehaviour
{
    [SerializeField]
    private Health health;

    private float timer = 0.0f;
    private bool hasFired = false;

    [SerializeField]
    private int turretLevel;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && hasFired == false && turretLevel == 1)
        {
            //health = other.GetComponent<Health>();
            other.GetComponent<Health>().TakeDamage(5);
            timer = 3;
            Debug.Log("Fire " + other);

            hasFired = true;
        }
        else if (other.tag == "Enemy" && hasFired == false && turretLevel == 2)
        {
            health = other.GetComponent<Health>();
            health.TakeDamage(5);
            timer = 2;
            Debug.Log("Fire");

            hasFired = true;
        }
        else if (other.tag == "Enemy" && hasFired == false && turretLevel == 3)
        {
            health = other.GetComponent<Health>();
            health.TakeDamage(5);
            timer = 1;
            Debug.Log("Fire");

            hasFired = true;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        turretLevel = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        else
        {
            hasFired = false;
        }



    }

   

}
