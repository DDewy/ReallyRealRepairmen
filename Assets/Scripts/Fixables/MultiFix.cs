using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFix : MonoBehaviour 
{

	// Use this for initialization
	private int fixNum = 0;
	public void AddFix()
	{
		fixNum++;

		if (fixNum == 2)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

}
