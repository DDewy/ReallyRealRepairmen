using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    [Header("Components")]
    public Camera _cam;
    public Rigidbody _rb;

    private Transform _camTrans;

	// Use this for initialization
	void Start ()
    {
        _camTrans = _cam.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");


    }
}
