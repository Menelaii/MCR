using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractableWithSword
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _acid;

    private Vector2 _velocity;
    private bool _destroySwordOnInteract;

    private void FixedUpdate()
    {
        _rigidBody.velocity = _velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractableWithTouch>(out IInteractableWithTouch IInteractable))
        {
            IInteractable.Interact();
        }
        
        Destroy(gameObject);
    }

    public void SetVelocity(Vector2 direction, float speed)
    {
        _velocity = direction * speed;
        Rotate(direction);
    }

    public void SetStats(bool destroySwordOnInteract)
    {
        _destroySwordOnInteract = destroySwordOnInteract;

        if (_destroySwordOnInteract)
            _renderer.color = _acid;
    }

    private void Rotate(Vector2 direction)
    {
        if (direction == Vector2.left)
            _renderer.flipX = false;
        else
            _renderer.flipX = true;
    }

    public void Interact(Sword sword)
    {
        if (_destroySwordOnInteract)
        {
            sword.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        _velocity *= -1;
        Rotate(_velocity.normalized);//sword.transform.right;
    } 
}
