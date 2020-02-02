using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {

	public static PhoneManager instance;

	public GameObject TextMsgPrefab;
	private GameObject MessageContainer;
	private int NumOfTexts;
	private float textLineHeight;
	private float messageSpacing;
	private int lineNum;
	private AudioSource messageSound;
	private Animator phoneAnim;
	private Animator strikeAnim;
	private Text taskText; 
	private AudioSource taskSound;
	private RectTransform strikeThru;
	private AudioSource strikeSound;
	public bool isStowed = true;



	void Awake()
	{
		if(instance == null||instance==this)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("Deleting the PhoneManager");
			DestroyImmediate(this);
			return;
		}

		//Get all References
		MessageContainer = this.transform.GetChild(0).gameObject;
		NumOfTexts = MessageContainer.transform.childCount;	
		messageSound = this.GetComponent<AudioSource>();	
		phoneAnim = this.GetComponent<Animator>();
		taskText = gameObject.transform.parent.GetChild(0).GetComponentInChildren<Text>();
		taskSound = taskText.gameObject.GetComponent<AudioSource>();
		strikeThru = taskText.gameObject.transform.parent.GetChild(1).GetComponent<RectTransform>();
		strikeAnim = strikeThru.gameObject.GetComponent<Animator>();
		strikeSound = strikeThru.gameObject.GetComponent<AudioSource>();
	}

	public void bringUpPhone()
	{
		isStowed = false;
		phoneAnim.SetTrigger("BringUpPhone");
	}

	public void putPhoneAway()
	{
		isStowed = true;
		phoneAnim.SetTrigger("PutAwayPhone");
	}

	public void setTask(string newTask)
	{
		strikeThru.gameObject.SetActive(false);
		taskSound.Play();
		taskText.text = newTask;
	}

	public void taskComplete()
	{		
		strikeThru.gameObject.SetActive(true);
		strikeSound.Play();
		strikeAnim.SetTrigger("Strikethru");
	}

	public void ClearMessages()
	{
		NumOfTexts = 0;
		if (MessageContainer.transform.childCount != 0)
		{
			for(int i = 0; i < MessageContainer.transform.childCount; i++)
			{
				Destroy(MessageContainer.transform.GetChild(i).gameObject);
			}
		}		
	}

	public void SendMultipleMessages(string[] newMsgs)
	{
		StartCoroutine(DelayMessages(newMsgs));
	}

	public void SendMultipleMessages(TaskObject.TxtMsg[] newMsgs)
	{
		StartCoroutine(DelayMessages(newMsgs));
	}

	IEnumerator DelayMessages(string[] newMsgs)
	{
		foreach(string msg in newMsgs)
		{
			NewTextMessage(msg);
			yield return new WaitForSeconds(0.6f);
		}
	}

	IEnumerator DelayMessages(TaskObject.TxtMsg[] newMsgs)
	{
		foreach(TaskObject.TxtMsg msg in newMsgs)
		{
			NewTextMessage(msg.msg);
			yield return new WaitForSeconds(msg.delayAfter);
		}
	}

	public void NewTextMessage(string txtMsg)
	{
		if (isStowed == true) 
		{
			isStowed = false;
			phoneAnim.SetTrigger("MessageOpen");
		}
		else
		{
			phoneAnim.SetTrigger("PhoneShake");
		}

		if (NumOfTexts == 4) scrollTexts();

		messageSound.Play();		

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

	public void scrollTexts()
	{
		NumOfTexts--;

		Destroy(MessageContainer.transform.GetChild(0).gameObject);

		for(int i = 0; i < MessageContainer.transform.childCount; i++)
		{
			RectTransform tempTxt = MessageContainer.transform.GetChild(i).GetComponent<RectTransform>();
			tempTxt.anchoredPosition = new Vector2(tempTxt.anchoredPosition.x, tempTxt.anchoredPosition.y + messageSpacing);
		}

	}
}
