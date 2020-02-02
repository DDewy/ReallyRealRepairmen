using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class PlayableDirectorEvents : MonoBehaviour {

	public PlayableDirector director;

	public UnityEvent OnFinished = new UnityEvent();

	private bool hasPlayed = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(hasPlayed == false)
		{
			if(director.state == PlayState.Playing)
			{
				hasPlayed = true;
			}
		}
		else
		{
			if(director.state == PlayState.Paused)
			{
				//Has finished
				Debug.Log("Director finished");
				this.enabled = false;
				OnFinished.Invoke();
			}
		}
	}
}
