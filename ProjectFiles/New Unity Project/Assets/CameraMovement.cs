using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{


    public float sensitivity = 1;
    public float mousewheelSensitivity = 1;

    [SerializeField] private float timeUntilDrag = .5f;

    private float touchTime = 0;
    private bool isDraggin = false;
    private Vector3 cameraStartPos, clickStartPos;
    private Camera cam;
    private float fov;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        fov = cam.orthographicSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleDrag();
    }

    private void HandleDrag()
    {
        if(GameManager.Instance.OpenWindwos.Count > 0)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            touchTime = 0;
        }
        else if (Input.GetMouseButton(0) && timeUntilDrag > touchTime)
        {
            touchTime += Time.deltaTime;
        }
        else if (Input.GetMouseButton(0) && timeUntilDrag < touchTime)
        {
            DragCamera();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDraggin = false;
            touchTime = 0;
        }
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cam.orthographicSize = fov + Input.GetAxis("Mouse ScrollWheel") * mousewheelSensitivity;
            fov = cam.orthographicSize;
        }
    }

    private void DragCamera()
    {
        if(!isDraggin)
        {
            cameraStartPos = transform.position;
            clickStartPos = Input.mousePosition;
        }
        isDraggin = true;
        transform.position = cameraStartPos + (clickStartPos - Input.mousePosition)* sensitivity;
        //clickStartPos = transform.position;
    }
}
