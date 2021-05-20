using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float _roomHeight;
    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private int _roomsPerTick;
    [SerializeField] private float _rightLadderX;//на Transform заменить
    [SerializeField] private float _leftLadderX;//
    [SerializeField] private float _ladderHeight;
    [SerializeField] private Ladder _ladder;

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
            int randomIndex = Random.Range(0, _rooms.Length);
            if (randomIndex == _lastRoomIndex)
            {
                randomIndex = Mathf.Clamp(randomIndex++, 0, _rooms.Length);
            }

            Vector3 spawnPosition = Vector3.up * _roomsCount * _roomHeight;
            Instantiate(_rooms[randomIndex], spawnPosition, Quaternion.identity);
            Vector3 ladderSpawnPosition = spawnPosition;
            ladderSpawnPosition.y -= _ladderHeight;

            if (_roomsCount % 2 == 0)
            {
                ladderSpawnPosition.x = _rightLadderX;
            }
            else
            {
                ladderSpawnPosition.x = _leftLadderX;
            }

            Instantiate(_ladder, ladderSpawnPosition, Quaternion.identity);//
            _roomsCount++;
            _lastRoomIndex = randomIndex;
        }
    }
}
