using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _cooldownSpread;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _workTimeWithoutHit;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _checkRayLength;

    private float _timer;
    private float _hitRemember;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * -1, _checkRayLength, _playerMask);
        _hitRemember -= Time.deltaTime;
        if (hit)
        {
            _hitRemember = _workTimeWithoutHit;
        }

        _timer -= Time.deltaTime;
        if(_timer <= 0 && _hitRemember > 0)
        {
            ResetTimer();
            Bullet bullet = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
            bullet.SetVelocity(transform.right * -1, _bulletSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * - 1 * _checkRayLength);
    }

    private void ResetTimer()
    {
        _timer = _cooldown + Random.Range(0, +_cooldownSpread);
    }
}
