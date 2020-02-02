using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackTurret : MonoBehaviour
{
    [SerializeField]
    private Health health;

    private float timer = 0.0f;
    private bool hasFired = false;

    [SerializeField]
    private int turretLevel;




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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && hasFired == false && turretLevel == 1)
        {
            //health = other.GetComponent<Health>();
            other.GetComponent<Health>().TakeDamage(10);
            timer = 10;
            Debug.Log("Fire " + other);

            hasFired = true;
        }
        else if (other.tag == "Enemy" && hasFired == false && turretLevel == 2)
        {
            health = other.GetComponent<Health>();
            health.TakeDamage(5);
            timer = 8;
            Debug.Log("Fire");

            hasFired = true;
        }
        else if (other.tag == "Enemy" && hasFired == false && turretLevel == 3)
        {
            health = other.GetComponent<Health>();
            health.TakeDamage(5);
            timer = 6;
            Debug.Log("Fire");

            hasFired = true;
        }
    }

}
