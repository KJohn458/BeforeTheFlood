using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health;
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        enemy = gameObject;
        enemy.GetComponent<Rigidbody>().AddForce(transform.right * 50);
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            Destroy(gameObject);
        }
    }

    public void subHealth()
    {
        health -= 1; 
    }
}
