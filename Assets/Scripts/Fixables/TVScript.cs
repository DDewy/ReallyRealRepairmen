using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScript : MonoBehaviour 
{

	// Use this for initialization
	private int fixNum = 0;
	public void FixTV()
	{
		fixNum++;

		if (fixNum == 2)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

}
