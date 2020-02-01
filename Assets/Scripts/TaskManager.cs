using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour 
{
	[Header("Scene References")]
	public DoorManager doorManager;
	public FixableManager fixableManager;

	private PhoneManager _phoneManager;

	[Header("Tasks")]
	public TaskObject[] tasksToDo;

	private int _currentIndex = 0;

	private bool taskActive = false;


	// Use this for initialization
	IEnumerator Start () 
	{
		//Save reference to the phone manager
		_phoneManager = PhoneManager.instance;

		//Wait for 2 seconds before showing the first task
		yield return new WaitForSeconds(2f);

		//Set the task manager off onto the first task
		SetTask(0);
	}

	void SetTask()
	{
		SetTask(_currentIndex);
	}
	
	void SetTask(int taskIndex)
	{
		if(taskActive)
		{
			//We need to remove any listeners to any task events from this task	
			StopListeningToTask(_currentIndex);
		}

		_currentIndex = taskIndex;

		TaskObject task = tasksToDo[_currentIndex];
		
		Fixable fixable = fixableManager.GetFixable(task.fixableType);
		//Set object as broken
		fixable.Break();

		//Listen for when it gets fixed
		fixable.OnFixed.AddListener(TaskCompleted);

		//Open any doors we need to for this 
		ExecuteDoorCommands(task.roomCommandsOnStart);

		//Set the main task for the player to do right now
		_phoneManager.setTask(task.TaskName);

		//Send the messages needed
		_phoneManager.ClearMessages();
		_phoneManager.SendMultipleMessages(task.TaskMessages);
	}

	void SetTask(TaskObject taskObject)
	{
		int instanceID = taskObject.GetInstanceID();
		int index = -1;
		for(int i = 0; i < tasksToDo.Length; i++)
		{
			if(instanceID == tasksToDo[i].GetInstanceID())
			{
				index = i;
				break;
			}
		}

		if(index != -1)
		{
			SetTask(index);
		}
		else
		{
			Debug.LogErrorFormat("Can not set task {0}, it is not in the ToDoTask array on the taskManager, please set before asking to set this task", taskObject.name);
		}
	}

	void TaskCompleted()
	{
		var task = tasksToDo[_currentIndex];

		//Open any doors we need to for this task
		ExecuteDoorCommands(task.roomCommandsOnFix);

		//Strike through the current task
		_phoneManager.taskComplete();

		//Stop Listening to this task
		StopListeningToTask(_currentIndex);

		//Set start the next tasks
		//TODO: Might need a delay here
		_currentIndex++;
		if(_currentIndex < tasksToDo.Length)
		{
			Invoke(nameof(SetTask), 2f);
		}
		else
		{
			Debug.Log("All of the tasks have been completed");

			_phoneManager.ClearMessages();
			_phoneManager.SendMultipleMessages(new string[] {"We good fam", "Now we get to leave"});
		}
	}

	void StopListeningToTask(int index)
	{
		var task = tasksToDo[index];
		var fixable = fixableManager.GetFixable(task.fixableType);
		fixable.OnFixed.RemoveListener(TaskCompleted);
	}

	void ExecuteDoorCommands(TaskObject.RoomCommand[] commands)
	{
		for(int c = 0; c < commands.Length; c++)
		{
			doorManager.SetDoors(commands[c].room, commands[c].OpenDoors);
		}
	}
}
