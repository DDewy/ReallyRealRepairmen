using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
	public bool allowInteract = true;

	public bool highlighted = false;

	public float InteractTime = 0.5f;


	public InteractEvent OnInteractSuccess = new InteractEvent();
	public UnityEvent OnInteract = new UnityEvent();

	public UnityEvent OnHighlight = new UnityEvent();

	#if TEST_HIGHLIGHT
	[Header("Testing")]
	public Material highlightMaterial;
	public MeshRenderer meshRenderer;
	private Material _defaultMaterial;
	
	private void Start() 
	{
		_defaultMaterial = meshRenderer.sharedMaterial;
	}
	#endif

	public void SetAllowInteraction(bool b)
	{
		allowInteract = b;
	}

	public bool AllowInteraction()
	{
		return allowInteract;
	}

	public bool Highlight()
	{
		if(allowInteract)
		{
			highlighted = true;
			OnHighlight.Invoke();

			#if TEST_HIGHLIGHT
			meshRenderer.sharedMaterial = highlightMaterial;
			#endif
		}

		return allowInteract;
	}

	public void RemoveHighlight()
	{
		highlighted = false;

		#if TEST_HIGHLIGHT
		meshRenderer.sharedMaterial = _defaultMaterial;
		#endif
	}
	
	public bool AttemptInteraction()
	{
		if(allowInteract)
		{
			Debug.Log("I have been interacted with", gameObject);

			OnInteractSuccess.Invoke(this);
			OnInteract.Invoke();
		}
		else
		{
			Debug.Log("Interaction attempt failed", gameObject);
		}
		
		return allowInteract;
	}

	public class InteractEvent : UnityEvent<Interactable> { }
}
