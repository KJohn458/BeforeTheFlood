using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3[] mousePosLastFrame = new Vector3[2];
    public GameObject pivot;
    public float dragSpeed, minZoom, maxZoom;
    Camera cam;
    Plane p;

    private void Start()
    {
        cam = Camera.main;
        p = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosLastFrame[0] = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            pivot.transform.eulerAngles += new Vector3(mousePosLastFrame[0].y - Input.mousePosition.y, mousePosLastFrame[0].x - Input.mousePosition.x);
            if (pivot.transform.eulerAngles.x > 350) pivot.transform.eulerAngles = new Vector3(-10, pivot.transform.eulerAngles.y);
            if (pivot.transform.eulerAngles.x < 280) pivot.transform.eulerAngles = new Vector3(-80, pivot.transform.eulerAngles.y);
            mousePosLastFrame[0] = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(1))
        {
            mousePosLastFrame[1] = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
           
            if (Vector3.Distance(mousePosLastFrame[1], Input.mousePosition) > 0) {
                Ray ray1 = cam.ScreenPointToRay(mousePosLastFrame[1]);
                Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
                float enter1, enter2;
                if (p.Raycast(ray1,out enter1) && p.Raycast(ray2,out enter2))
                {
                    Vector3 hit = ray1.GetPoint(enter1) - ray2.GetPoint(enter2);
                    pivot.transform.position += hit;
                }
            }
            mousePosLastFrame[1] = Input.mousePosition;
        }
        transform.localPosition = new Vector3(0, 0, Mathf.Clamp(transform.localPosition.z - Input.mouseScrollDelta.y, minZoom, maxZoom));
    }
}
