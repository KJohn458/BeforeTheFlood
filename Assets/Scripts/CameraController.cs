using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2[] mousePosLastFrame = new Vector2[2];
    public GameObject pivot;
    public float dragSpeed;
    Camera cam;

    Vector3 operator *()

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosLastFrame[0] = Input.mousePosition;
        } else if (Input.GetMouseButton(0))
        {
            pivot.transform.eulerAngles += new Vector3(mousePosLastFrame[0].y - Input.mousePosition.y, mousePosLastFrame[0].x - Input.mousePosition.x);
            mousePosLastFrame[0] = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(1))
        {
            mousePosLastFrame[1] = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 heading = pivot.transform.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            pivot.transform.forward = (heading / heading.magnitude)*-1;
            Vector3 inp = new Vector3(mousePosLastFrame[1].x - Input.mousePosition.x, 0, mousePosLastFrame[1].y - Input.mousePosition.y);
            pivot.transform.position += (inp * pivot.transform.forward * dragSpeed);
            mousePosLastFrame[1] = Input.mousePosition;
        }
    }
}
