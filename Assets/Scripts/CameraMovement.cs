using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _roomsHeight = 5;
    [SerializeField] private float _relocateSpeed;

    private Vector2 _newPosition;
    private Coroutine _previousTask;

    public void OnLadderPassed()
    {
        if (_previousTask != null)
            StopCoroutine(_previousTask);

        _newPosition = new Vector3(transform.position.x, transform.position.y + _roomsHeight, transform.position.z);
        _previousTask = StartCoroutine(Relocate());
    }

    private IEnumerator Relocate()
    {
        float t = 0;
        while (true)
        {
            t += _relocateSpeed;
            Vector2 newPosition = Vector3.Lerp(transform.position, _newPosition, t * Time.deltaTime);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            yield return null;
        }
    }
}
