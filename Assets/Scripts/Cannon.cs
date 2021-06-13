using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _cooldownSpread;
    [SerializeField] private float _workTimeWithoutHit;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _checkRayLength;
    [SerializeField] private float _stopShootingRayLength;

    [Header("Bullet Type")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private bool _bulletNotInteractable;
    [SerializeField] private bool _bulletDestroySword;
    [SerializeField] private bool _randomType;

    private float _timer;
    private float _hitRemember;

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.right * -1, _stopShootingRayLength, _playerMask))
        {
            _hitRemember = 0;
            _timer = 0;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * -1, _checkRayLength, _playerMask);
        _hitRemember -= Time.deltaTime;
        if (hit)
        {
            _hitRemember = _workTimeWithoutHit;
        }

        _timer -= Time.deltaTime;
        if(_timer <= 0 && _hitRemember > 0)
        {
            Fire();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * - 1 * _checkRayLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * -1 * _stopShootingRayLength);
    }

    private void ResetTimer()
    {
        _timer = _cooldown + Random.Range(0, +_cooldownSpread);
    }

    private void Fire()
    {
        ResetTimer();
        Bullet bullet = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
        bullet.SetVelocity(transform.right * -1, _bulletSpeed);
        
        if (_randomType)
        {
            int random = Random.Range(0, 10);
            if (random % 2 == 0)
            {
                _bulletDestroySword = true;
                _bulletNotInteractable = false;
            }
            else if (random < 6)//доля обычных, вывести в переменную
            {
                _bulletDestroySword = false;
                _bulletNotInteractable = false;
            }
            else
            {
                _bulletDestroySword = false;
                _bulletNotInteractable = true;
            }
        }

        bullet.SetStats(_bulletNotInteractable, _bulletDestroySword);
    }
}
