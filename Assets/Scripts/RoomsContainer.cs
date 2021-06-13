using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomsContainer")]
public class RoomsContainer : ScriptableObject
{
    [SerializeField] private float _roomHeight;
    [SerializeField] private Room[] _rooms;//чётный индекс => лестница слева

    public float RoomHeight => _roomHeight;

    public Room[] GetRooms()
    {
        var roomsCopy = new Room[_rooms.Length];
        for (int i = 0; i < roomsCopy.Length; i++)
        {
            roomsCopy[i] = _rooms[i];
        }

        return roomsCopy;
    }
}
