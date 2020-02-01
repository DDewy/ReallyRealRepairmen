using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {


	public GameObject TextMsgPrefab;
	private GameObject MessageContainer;
	private int NumOfTexts;
	private float textLineHeight;
	private float messageSpacing;
	public string[] dialogue;
	private int lineNum;


	// Use this for initialization
	void Start () {

		MessageContainer = this.transform.GetChild(0).gameObject;
		NumOfTexts = MessageContainer.transform.childCount;		
	}
	
	// Update is called once per frame
	void Update () 
	{		

		if (Input.GetKeyDown("space"))
		{
			NewTextMessage(dialogue[Random.Range(0, dialogue.Length-1)]);
		}

	}

	public void SendMultipleMessages(string[] newMsgs)
	{
		StartCoroutine(DelayMessages(newMsgs));
	}

	IEnumerator DelayMessages(string[] newMsgs)
	{
		foreach(string msg in newMsgs)
		{
			NewTextMessage(msg);
			yield return new WaitForSeconds(0.2f);
		}
	}

	public void NewTextMessage(string txtMsg)
	{
		GameObject newText;
		newText = Instantiate(TextMsgPrefab);
		RectTransform newTextT = newText.GetComponent<RectTransform>();

		//calc line num based on length of txtMsg
		textLineHeight = newTextT.sizeDelta.y;
		messageSpacing = textLineHeight + 4;

		lineNum = 1;

		newTextT.sizeDelta = new Vector2(newTextT.sizeDelta.x, (newTextT.sizeDelta.y*lineNum));
		newText.GetComponentInChildren<Text>().text = txtMsg;

		newText.transform.SetParent(MessageContainer.transform, false);
		newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (NumOfTexts*-messageSpacing));

		NumOfTexts++;
	}
}
