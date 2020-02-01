using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewTask", menuName = "RepairMan/Task", order = 1)]
public class TaskObject : ScriptableObject {
	/// <summary>
	/// The Rooms to open or close when the task is started
	/// </summary>
	public RoomCommand[] roomCommandsOnStart;

	public RoomCommand[] roomCommandsOnFix;

	/// <summary>
	/// Type of object that needs to be fixed
	/// </summary>
	public Repairables fixableType;

	public string[] TaskMessages;

	public string TaskName;

	[System.Serializable]
	public struct RoomCommand
	{
		public Rooms room;
		public bool OpenDoors;
	}
}
