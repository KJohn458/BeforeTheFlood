using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLoopActive : MonoBehaviour
{
    public ParticleSystem ps;

    private void OnEnable()
    {
        ps.Play();
        Debug.Log("Please");
    }

    void Update()
    {
        if (!ps.isPlaying)
        {
            ps.gameObject.SetActive(false);
        }
    }
}
