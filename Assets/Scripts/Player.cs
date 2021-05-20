using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private LevelGenerator _generator;

    public UnityAction OnLadderPassed;

    private void OnEnable()
    {
        OnLadderPassed += _camera.OnLadderPassed;
        OnLadderPassed += _generator.OnLadderPassed;
    }

    private void OnDisable()
    {
        OnLadderPassed -= _camera.OnLadderPassed;
        OnLadderPassed -= _generator.OnLadderPassed;
    }
}
