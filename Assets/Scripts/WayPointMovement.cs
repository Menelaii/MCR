using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _waveMovement;
    [SerializeField] private float _waveMovementOffsetY;
    [SerializeField] private Transform _path;
    [SerializeField] private SpriteRenderer _renderer;

    private Transform[] _points;
    private int _currentPoint;
    private float _time;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;

        Transform target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        if (_waveMovement)
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(_time) * _waveMovementOffsetY, transform.position.z);

        if (transform.position == target.position && _waveMovement == false)
            NextPoint();
        else if(_waveMovement && transform.position.x == target.position.x)
            NextPoint();
    }

    private void NextPoint()
    {
        _renderer.flipX = !_renderer.flipX;

        _currentPoint++;
        if (_currentPoint >= _points.Length)
        {
            _currentPoint = 0;
        }
    }
}
