using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Door : MonoBehaviour {

	public Animator doorAnimator;

	public bool startOpen = false;

	public bool openClockwise = true;

	[Header("Audio")]
	public AudioSource doorAudio;

	public AudioClip openClip;
	public AudioClip closeClip;
	private bool isOpen = false;

	private void Start() {
		if(startOpen)
			OpenDoor();
	}

	public void OpenDoor()
	{
		isOpen = true;

		doorAnimator.SetBool("isClockwise", openClockwise);
		doorAnimator.SetTrigger("Open");

		doorAudio.clip = openClip;
		doorAudio.Play();
	}

	public void CloseDoor()
	{
		isOpen = false;
		
		doorAnimator.SetBool("isClockwise", openClockwise);
		doorAnimator.SetTrigger("Close");

		doorAudio.clip = closeClip;
		doorAudio.Play();
	}

	public void ToggleDoor()
	{
		if(isOpen)
		{
			CloseDoor();
		}
		else
		{
			OpenDoor();
		}
	}

	#if UNITY_EDITOR
	[CustomEditor(typeof(Door))]
	public class Inspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			GUILayout.Space(5f);


			if(GUILayout.Button("Toggle Door"))
			{
				Door script = (Door)target;

				script.ToggleDoor();
			}
		}
	}
	#endif
}
