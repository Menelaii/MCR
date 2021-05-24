using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float _roomHeight;
    [SerializeField] private int _roomsPerTick;
    [Header("чётный индекс => лестница слева")]
    [SerializeField] private GameObject[] _rooms;

    private int _roomsCount;
    private int _lastRoomIndex;

    private void Start()
    {
        _roomsCount = 1;
        GenerateRooms(_roomsPerTick);
    }

    public void OnLadderPassed()
    {
        GenerateRooms(_roomsPerTick);
    }

    private void GenerateRooms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, _rooms.Length - 1);
            if(_roomsCount % 2 == 0 && randomIndex % 2 == 0)
            {
                randomIndex++;
            }
            else if(_roomsCount % 2 != 0 && randomIndex % 2 != 0)
            {
                randomIndex++;
            }

            if (randomIndex - _lastRoomIndex <= 1)
            {
                randomIndex += 2;
            }

            if (randomIndex >= _rooms.Length)
            {
                if (randomIndex % 2 == 0)
                {
                    randomIndex = 0;
                }
                else
                {
                    randomIndex = 1;
                }
            }

            Vector3 spawnPosition = Vector3.up * _roomsCount * _roomHeight;
            Instantiate(_rooms[randomIndex], spawnPosition, Quaternion.identity);
            _roomsCount++;
            _lastRoomIndex = randomIndex;
        }
    }
}
