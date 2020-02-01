using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackTurret : MonoBehaviour
{
    [SerializeField]
    private BasicEnemy enemy;

    private float timer = 0.0f;
    private bool hasFired = false;

    [SerializeField]
    private int turretLevel;

    // Start is called before the first frame update
    void Start()
    {
        turretLevel = 0;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && hasFired == false && turretLevel == 1)
        {
            enemy = other.GetComponent<BasicEnemy>();
            enemy.subHealth();
            
            Debug.Log("Fire");

            hasFired = true;
        }
        else if (other.tag == "Enemy" && hasFired == false && turretLevel == 2)
        {
            enemy = other.GetComponent<BasicEnemy>();
            enemy.subHealth();

            Debug.Log("Fire");

            hasFired = true;
        }
        else if (other.tag == "Enemy" && hasFired == false && turretLevel == 3)
        {
            enemy = other.GetComponent<BasicEnemy>();
            enemy.subHealth();

            Debug.Log("Fire");

            hasFired = true;
        }
    }
}
