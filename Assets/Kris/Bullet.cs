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
        OnCollisionEnter(collision, level);
    }

    private void OnCollisionEnter(Collision collision, int level)
    {
        collision.gameObject.GetComponent<Health>()?.TakeDamage(level);
        Destroy(gameObject);

    }

    public void setLevel(int x)
    {
        level = x;
    }
}
