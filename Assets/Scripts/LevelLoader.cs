using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public string[] LevelNames;
	public int ActiveSceneIndex = 0;

	// Use this for initialization
	IEnumerator Start () 
	{
		for(int i = 0; i < LevelNames.Length; i++)
		{
			yield return SceneManager.LoadSceneAsync(LevelNames[i], LoadSceneMode.Additive);
		}

		//Set the Level Active
		var activeScene = SceneManager.GetSceneByName(LevelNames[ActiveSceneIndex]);
		SceneManager.SetActiveScene(activeScene);

		//Remove this Loading Scene
		SceneManager.UnloadSceneAsync(gameObject.scene.buildIndex);
	}
}
