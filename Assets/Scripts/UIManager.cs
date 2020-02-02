using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameplayScreen gameplayScreen;
	
	[System.Serializable]
	public class GameplayScreen
	{
		public GameObject screenRoot;
		public Image ProgressBG;
		public Image ProgressCircle;
		public Image HighlightImg;
	}
}
