using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{



    public float sensitivity = 1;
    public float mousewheelSensitivity = 1;

    [SerializeField] private CinemachineVirtualCamera townCam;
    [SerializeField] private Collider2D townBounds;
    [SerializeField] private float timeUntilDrag = .5f;

    private float touchTime = 0;
    private bool isDraggin = false;
    private Vector3 cameraStartPos, clickStartPos;
    private Camera cam;
    private float fov;


    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponent<Camera>();
        fov = townCam.m_Lens.OrthographicSize;
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
            //Move Towards Middle of Town till in bounds
            while (!townBounds.bounds.Contains(townCam.transform.position))
            {
                townCam.transform.position -= (townCam.transform.position - townBounds.transform.position).normalized;
            }
        }
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            townCam.m_Lens.OrthographicSize = fov + Input.GetAxis("Mouse ScrollWheel") * mousewheelSensitivity;
            fov = townCam.m_Lens.OrthographicSize;
        }
    }

    private void DragCamera()
    {
        if(!isDraggin)
        {
            cameraStartPos = townCam.transform.position;
            clickStartPos = Input.mousePosition;
        }
        Debug.Log("Is Draggin");
        isDraggin = true;
        townCam.transform.position = cameraStartPos + (clickStartPos - Input.mousePosition)* sensitivity;
        //clickStartPos = transform.position;
    }
}
