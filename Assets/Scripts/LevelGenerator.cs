using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int _startDifficulty;
    [SerializeField] private int _roomsOnStart;
    [SerializeField] private int _roomsPerTick;
    [SerializeField] private int _roomsToIncreaseDifficulty;
    [SerializeField] private RoomsContainer _container;
    [SerializeField] private Room _lastRoom;

    private Room[] _rooms;
    private List<Room> _spawnList;
    private float _roomHeight;
    private int _spawnedRooms;
    private int _difficulty;
    private int _maxCurrentDifficultyOfRooms;

    private void Awake()
    {
        _roomHeight = _container.RoomHeight;
        _rooms = _container.GetRooms();
        _spawnList = new List<Room>();
        _difficulty = _startDifficulty;
        _currentDifficulty = _difficulty;
        SpawnRooms(_roomsOnStart);
    }

    public void OnLadderPassed()
    {
        if (_spawnedRooms >= _roomsToIncreaseDifficulty)
        {
            _difficulty++;
            _roomsToIncreaseDifficulty += _roomsToIncreaseDifficulty;
        }

        SpawnRooms(_roomsPerTick);
    }

    private void TryReformSpawnList()
    {
        if (_spawnList.Count == 0 || _currentDifficulty != _difficulty)
        {
            ReformSpawnList();
        }
    }

    private void ReformSpawnList()
    {
        _currentDifficulty = _difficulty;
        _spawnList.Clear();
        for (int i = 0; i < _rooms.Length; i++)
        {
            TryAddToSpawnList(_rooms[i]);
        }

        ShuffleSpawnList();
        SortSpawnList();
    }

    private void TryAddToSpawnList(Room room)
    {
        if (room.NeedSword && _playerHaveSword == false)
        {
            return;
        }
        else if(_currentDifficulty >= room.Difficulty)
        {
            _spawnList.Add(room);
        }
    }

    private void ShuffleSpawnList()
    {
        for (int i = _spawnList.Count - 1; i >= 1; i--)
        {
            int randnumb = Random.Range(0, i + 1);
            Room temp = _spawnList[randnumb];
            _spawnList[randnumb] = _spawnList[i];
            _spawnList[i] = temp;
        }
    }

    private void SortSpawnList()
    {
        var sortedList = new List<Room>(_spawnList.Count);

        var rightLadderRooms = _spawnList.Where(room => room.IsLadderOnRight).ToArray();

        var leftLadderRooms = _spawnList.Where(room => room.IsLadderOnRight == false).ToArray();

        for (int i = 0; i < _spawnList.Count; i++)
        {
            int evenCounter = 0;
            int oddCounter = 0;
            if(i % 2 == 0)
            {
                sortedList.Add(leftLadderRooms[evenCounter]);
                evenCounter++;
            }
            else
            {
                sortedList.Add(rightLadderRooms[oddCounter]);
                oddCounter++;
            }
        }
        _spawnList = sortedList;
    }

    private void SpawnRoomFromList()
    {
        Room roomToSpawn = _spawnList[0];
        _spawnList.RemoveAt(0);
        Vector3 spawnPosition = Vector3.up * (_spawnedRooms + 1) * _roomHeight;
        Instantiate(roomToSpawn, spawnPosition, Quaternion.identity);
        _spawnedRooms++;
    }

    private void SpawnRooms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            TryReformSpawnList();
            SpawnRoomFromList();
        }
    }
}
