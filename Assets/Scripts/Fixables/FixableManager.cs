using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixableManager : MonoBehaviour {

	[Tooltip("The root of all fixable objects, if set this will only find fixable objects underneath this object, else it will check through the whole scene")]
	public GameObject FixablesRoot;
	private Fixable[] fixables;

	// Use this for initialization
	void Start () 
	{
		List<Fixable> fixes = new List<Fixable>();

		//If the fixables root is not null we use that, else we check the entire scene
		GameObject[] rootObjects = FixablesRoot == null ? new GameObject[]{FixablesRoot} : gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < rootObjects.Length; i++)
		{
			Fixable[] findFix = rootObjects[i].GetComponentsInChildren<Fixable>();

			if(findFix.Length > 0)
			{
				fixes.AddRange(findFix);
			}
		}

		this.fixables = fixes.ToArray();
	}

	public Fixable GetFixable(Repairables type)
	{
		for(int i = 0; i < fixables.Length; i++)
		{
			if(fixables[i].repairType == type)
			{
				return fixables[i];
			}
		}

		Debug.LogFormat("Could not find {0} in the fixables list, please add a {0} into the scene", type.ToString());
		return null;
	}
}
