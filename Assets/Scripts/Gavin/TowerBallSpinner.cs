using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TowerBallSpinner : MonoBehaviour
{

    public Animator anim;

    void Awake()
    {
        transform.DOLocalRotate(new Vector3(60f, 0.0f, 0.0f), 1.0f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //transform.DOPunchScale(new Vector3(-.7f, -.7f, -.7f), 1, 0, 0);
            anim.SetTrigger("Shoot");
        }
    }
}
