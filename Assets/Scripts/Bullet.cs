using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;

    private Vector2 _velocity;

    private void FixedUpdate()
    {
        _rigidBody.velocity = _velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void SetVelocity(Vector2 direction, float speed)
    {
        _velocity = direction * speed;
    }

    public void Interact()
    {
        _velocity *= -1;
    }
}
