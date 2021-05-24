using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [Header("Local position Rx = 1.12 , Lx = -7.6 , Y = -1.5")]
    [SerializeField] private float _velocity;

    private Rigidbody2D _rigidbody;
    private bool _active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_active == false)
            return;

        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.transform.position = new Vector2(transform.position.x, playerMovement.transform.position.y);
            playerMovement.enabled = false;
            _rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            _rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_active == false)
            return;

        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.enabled = true;
            playerMovement.ChangeMoveDirectionX();
            _rigidbody.velocity = Vector2.zero;
            _rigidbody = null;
            _active = false;
            enabled = false;

            playerMovement.transform.GetComponent<Player>().OnLadderPassed?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null)
            return;

        _rigidbody.velocity = Vector2.up * _velocity;
    }
}
