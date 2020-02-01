using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

	[SerializeField] private RoomInfo[] rooms;

	private Dictionary<Rooms, Door[]> _rooms;

	private void OnEnable() 
	{
		_rooms = new Dictionary<Rooms, Door[]>(rooms.Length);

		for(int i = 0; i < rooms.Length; i++)
		{
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
	}

	public void SetDoors(Rooms room, bool setOpen)
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
