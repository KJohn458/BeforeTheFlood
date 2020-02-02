using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLoopActive : MonoBehaviour
{
    public ParticleSystem ps;
    
    private float timer;
    private float buffer = 10f;

    private void OnEnable()
    {
        ps.Play();
        timer = Time.time;
    }

    void Update()
    {
        if (Time.time - timer >= buffer)
        {
            ps.gameObject.SetActive(false);
        }
    }
}
