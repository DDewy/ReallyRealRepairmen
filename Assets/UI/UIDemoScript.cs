using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDemoScript : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{		
		if (Input.GetKeyDown("space"))
		{
			PhoneManager.instance.setTask("Press D to test the UI");
			PhoneManager.instance.SendMultipleMessages(new string[2]{"Hello!", "I'm a string"});
		}

		if (Input.GetKeyDown("d"))
		{
			PhoneManager.instance.taskComplete();
		}

		if (Input.GetKeyDown("f"))
		{
			if (PhoneManager.instance.isStowed)
			{
				PhoneManager.instance.bringUpPhone();
			}
			else
			{
				PhoneManager.instance.putPhoneAway();
			}			
		}

	}
}
