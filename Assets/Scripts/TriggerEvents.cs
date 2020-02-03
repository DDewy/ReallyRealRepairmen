using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour {

	public UnityEvent TriggerEnter = new UnityEvent();
	public UnityEvent TriggerExit = new UnityEvent();

	public UnityEvent CollisionEnter = new UnityEvent();

	public UnityEvent CollisionExit = new UnityEvent();

	private void OnTriggerEnter(Collider other) {
		TriggerEnter.Invoke();
	}

	private void OnTriggerExit(Collider other) {
		TriggerExit.Invoke();
	}

	private void OnCollisionEnter(Collision other) {
		CollisionEnter.Invoke();
	}

	private void OnCollisionExit(Collision other) {
		CollisionExit.Invoke();
	}
}
