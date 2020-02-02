using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour {

	public float rollSpeed = 10f;
	public RectTransform BackgroundRect;
	public RectTransform CreditTextRect;

	public bool rollOnStart = false;

	public Animator creditsAnimator;

	private void Start() {
		if(rollOnStart)
		{
			StartCoroutine(PlayCredits());
		}
	}

	public void PlayAnimCredits()
	{
		creditsAnimator.SetTrigger("RollCredits");
	}

	IEnumerator PlayCredits()
	{
		int ScreenHeight = Screen.height;

		bool rollingBG = true;
		bool rollingText = true;

		while(rollingBG || rollingText)
		{
			if(rollingBG)
			{
				Vector2 pos = BackgroundRect.anchoredPosition;
				pos.y += Time.deltaTime * rollSpeed;
				BackgroundRect.anchoredPosition = pos;
				if(BackgroundRect.rect.yMin < 0f)
				{
					Debug.LogFormat("The BG pos is {0}", pos);
					rollingBG = false;
				}
			}
			
			if(rollingText)
			{
				Vector2 pos = CreditTextRect.anchoredPosition;
				pos.y += Time.deltaTime * rollSpeed;
				CreditTextRect.anchoredPosition = pos;

				if(pos.y > CreditTextRect.rect.height)
				{
					Debug.Log("Credits are off screen");
					rollingText = false;
				}
			}
			
			yield return null;
		}

		Debug.Log("Finished rolling text");
	}
}
