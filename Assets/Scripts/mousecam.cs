﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousecam : MonoBehaviour
{

    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    // the chacter is the capsule
    public GameObject character;
    // get the incremental value of mouse moving
    public Vector2 mouseLook;
    // smooth the mouse moving
    private Vector2 smoothV;

    public bool trackMouse = true;

    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;

        LockMouse();

        //Set up the mouse look
        mouseLook.x = Quaternion.Angle(Quaternion.identity, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(trackMouse)
            {
                ReleaseMouse();
            }
            else
            {
                LockMouse();
            }
        }

        if(!trackMouse)
            return;

        // md is mosue delta
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        // the interpolated float result between the two float values
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        // incrementally add to the camera look
        mouseLook += smoothV;

        // vector3.right means the x-axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }

    private void OnDestroy() {
        ReleaseMouse();
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        Debug.LogFormat("Application Focus {0}", focusStatus);
        if(focusStatus)
        {
            //Lock the mouse
            LockMouse();
        }
        else
        {
            
        }
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        trackMouse = true;
    }

    private void ReleaseMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        trackMouse = false;
    }
}