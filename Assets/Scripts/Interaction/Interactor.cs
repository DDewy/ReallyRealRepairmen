using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
	//Change the enabled variable to disable and re-enable this

	[Header("Components")]
	public Transform CamTrans;

	[Header("Settings")]
	public float castRadius = 0.5f;

	public float interactRange = 5f;
	private int interactLayer;
	private int interactMask;

	private Interactable _lastInteract;
	
	// Use this for initialization
	void Start () 
	{
		interactLayer = LayerMask.NameToLayer("Interact");
		interactMask = 1 << interactLayer | 1 << LayerMask.NameToLayer("Floor") | 1 << LayerMask.NameToLayer("Default");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Interactable newInteract = null;

		//Attempt to find a interactable
		Ray ray = new Ray(CamTrans.position, CamTrans.forward);
		RaycastHit hitInfo;
		
		if(Physics.SphereCast(ray, castRadius, out hitInfo, interactRange, interactMask, QueryTriggerInteraction.Collide))
		{
			if(hitInfo.collider.gameObject.layer == interactLayer)
			{
				//Attempt to get the component
				newInteract = hitInfo.collider.gameObject.GetComponent<Interactable>();
			}
		}

		//If this new interact is valid and different from the last one 
		bool newInteractValid = newInteract != null;
		bool oldInteractValid = _lastInteract != null;

		//Check if we need to tell the old interact that it is no longer highlighted
		if(oldInteractValid && (!newInteract || (newInteract != _lastInteract)))
		{
			//If old is valid, and new is null or new isn't the same as old then we are telling the old that it is no longer highlighted
			_lastInteract.RemoveHighlight();
		}

		//Check if we need to highlight the current interact
		if(newInteractValid && (newInteract != _lastInteract))
		{
			newInteract.Highlight();
		}

		if(newInteractValid && Input.GetMouseButtonDown(0))
		{
			newInteract.AttemptInteraction();
		}

		_lastInteract = newInteract;
	}

    private void OnDrawGizmos() 
	{
		Gizmos.color = Color.red;
        Vector3 camPos = CamTrans.position;
		Gizmos.DrawLine(camPos, camPos + (CamTrans.forward * interactRange));
    }
}
