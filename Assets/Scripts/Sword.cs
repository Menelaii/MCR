using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    [SerializeField] private float _buttonPressedRememberTime;
    [SerializeField] private float _attackRememberTime;
    [SerializeField] private Transform _center;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _interactable;
    [SerializeField] private SpriteRenderer _renderer;

    private float _time;
    private float _buttonPressedRemember;
    private float _attackRemember;

    public float CoolDownTime => _coolDown;
    public float CoolDown => _time;

    private void Update()
    {
        _renderer.color = Color.white;

        _buttonPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A))
        {
            _buttonPressedRemember = _buttonPressedRememberTime;
        }

        _time += Time.deltaTime;
        if (_buttonPressedRemember > 0 && _time >= _coolDown)
        {
            _buttonPressedRemember = 0;
            _time = 0;

            _attackRemember = _attackRememberTime;
        }

        _attackRemember -= Time.deltaTime;
        if (_attackRemember > 0)
        {
            _buttonPressedRemember = _buttonPressedRememberTime;

            TryInteract();
            _renderer.color = Color.red; //_animator.Play("Attack");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_center.position, _radius);
    }

    private void TryInteract()
    {
        Collider2D collider = Physics2D.OverlapCircle(_center.position, _radius, _interactable);
        if (collider != null)
        {
            if (collider.transform.TryGetComponent<Box>(out Box box))
            {
                box.Interact();
            }
            else if (collider.transform.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Interact();
            }
            else if (collider.transform.TryGetComponent<Bullet>(out Bullet bullet))
            {
                bullet.Interact(transform.GetComponentInParent<Player>().transform);
            }
        }
    }
}
