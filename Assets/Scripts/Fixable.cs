using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fixable : MonoBehaviour {

	/// <summary>
	/// This will start as fixed first and then get broken, to be fixed
	/// </summary>
	public bool isFixed = true;

	public Repairables repairType;

	public UnityEvent OnBroken;
	public UnityEvent OnFixed;

	public void Fix() 
	{
		isFixed = true;

		OnFixed.Invoke();
	}

	public void Break()
	{
		isFixed = false;

		OnBroken.Invoke();
	}
}
