using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

	[SerializeField] private RoomInfo[] rooms;

	private Dictionary<Rooms, Door[]> _rooms;
	private bool _roomsSetUp = false;

	private void OnEnable() 
	{
		if(_roomsSetUp == false)
		{
			SetUpDictionary();
		}
	}

	public void SetDoors(Rooms room, bool setOpen)
	{
		if(_roomsSetUp == false)
		{
			//We need to set up this Dictionary
			SetUpDictionary();
		}

		if(_rooms.ContainsKey(room))
		{
			var doors = _rooms[room];

			for(int i = 0; i < doors.Length; i++)
			{
				if(setOpen)
				{
					doors[i].OpenDoor();
				}
				else
				{
					doors[i].CloseDoor();
				}
			}	
		}
		else
		{
			Debug.LogErrorFormat("Failed to find doors for room {0}", room);
		}
	}

	private void SetUpDictionary()
	{
		_rooms = new Dictionary<Rooms, Door[]>(rooms.Length);

		for(int i = 0; i < rooms.Length; i++)
		{
			Debug.LogFormat("Trying to add room {0} and it has {1} doors", rooms[i].name, rooms[i].doors.Length);
			if(_rooms.ContainsKey(rooms[i].name))
			{
				Debug.LogWarningFormat(gameObject, "There are two {0} rooms in the Rooms array, please fix this with index {1}", rooms[i].name, i);
				continue;
			}
			else
			{
				_rooms.Add(rooms[i].name, rooms[i].doors);
			}
		}

		_roomsSetUp = true;
	}

	[System.Serializable]
	public struct RoomInfo
	{
		public Rooms name;
		public Door[] doors;
	}
}
public enum Rooms
{
	Conservatory,
	DiningRoom,
	LivingRoom,
	Kitchen,
	Basement,
	Bathroom,
	SpareBedroom,
	BedroomBalcony,
	Garage
}
