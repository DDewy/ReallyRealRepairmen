using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskCompletedEvent : MonoBehaviour {

	public TaskManager taskManagerRef;

	public TaskObject TaskToInvokeOn;

	public UnityEvent OnTaskCompleted;

	private void Start() {
		taskManagerRef.OnTaskCompleted.AddListener(TaskCompleted);
	}

	private void TaskCompleted(TaskObject completedTask)
	{
		if(TaskToInvokeOn.GetInstanceID() == completedTask.GetInstanceID())
		{
			OnTaskCompleted.Invoke();
		}
	}
}
