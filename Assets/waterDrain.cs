using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDrain : MonoBehaviour
{
    private int level = 1;

    public void drain()
    {
        switch (level) {
            case (1):
                {
                    gameObject.transform.position -= Vector3.up * 12.5f;
                    level++;
                    break;
                }

            case (2):
                {
                    gameObject.transform.position -= Vector3.up * 15.5f;
                    level++;
                    break;
                }

            case (3):
                {
                    gameObject.transform.position -= Vector3.up * 10.2f;
                    level++;
                    break;
                }


        }
    }
}
