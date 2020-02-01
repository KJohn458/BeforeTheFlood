using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackTurret : MonoBehaviour
{
    [SerializeField]
    private BasicEnemy enemy;

    private float timer = 0.0f;
    private bool hasFired = false;
    //private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        //manager.getTimer(); or somethin like that
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
        if(other.tag == "Enemy")
        {
            enemy = other.GetComponent<BasicEnemy>();
            enemy.subHealth();
            
            Debug.Log("Fire");

            hasFired = true;
        }
    }
}
