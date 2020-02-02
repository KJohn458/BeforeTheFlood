using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float delay = 3.0f;
    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, delay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (level == 1)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
        else if (level == 2)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(2);
        }
        else if(level == 3)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(3);
        }

    }

    public void setLevel(int x)
    {
        level = x;
    }
}
