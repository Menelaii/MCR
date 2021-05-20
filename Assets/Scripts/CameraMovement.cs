using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _roomsHeight = 5;


    public void OnLadderPassed()
    {
        SetPositionOnNextStage();
    }

    private void SetPositionOnNextStage()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + _roomsHeight, transform.position.z);
    }
}
