using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    [Header("Components")]
    public Camera Cam;
    public Rigidbody RB;

    [Header("Movement")]
    public float MovementSpeed = 1f;

    [Header("Camera Settings")]
    public float PitchSensitivity = 1f;
    public float YawSensitivity = 1f;
    public bool PitchInvert = false;
    public bool YawInvert = false;

    private Transform _camTrans;
    private Transform _playerTrans;
    private Vector3 _movementDelta;

	// Use this for initialization
	void Start ()
    {
        _camTrans = Cam.transform;
        _playerTrans = RB.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        //Calculate the player movement
        _movementDelta += xMove * _playerTrans.right;
        _movementDelta += yMove * _playerTrans.forward;
        _movementDelta *= MovementSpeed * Time.deltaTime;
        
        //Calculate the camera rotation
        Quaternion playerRot = _playerTrans.localRotation;
        Quaternion camRot = _camTrans.localRotation;

        playerRot *= Quaternion.Euler(Vector3.up * (xMouse * YawSensitivity * (YawInvert ? -1f : 1f)));
        camRot *= Quaternion.Euler(Vector3.right * (yMouse * PitchSensitivity * (PitchInvert ? 1f : -1f)));

        //Set the player position
        _playerTrans.localRotation = playerRot;
        _camTrans.localRotation = camRot;
    }

    private void FixedUpdate() 
    {
        RB.MovePosition(RB.position + _movementDelta);
        _movementDelta = Vector3.zero;
    }
}
