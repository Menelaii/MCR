using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, ISleeping
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _cooldownSpread;
    [SerializeField] private float _awakenTime;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _checkRayLength;
    [SerializeField] private float _stopShootingRayLength;

    [Header("Bullet Type")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private bool _bulletDestroySword;
    [SerializeField] private bool _randomType;

    private float _timer;

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.right * -1, _stopShootingRayLength, _playerMask))
        {
            Sleep();
        }

        _timer -= Time.deltaTime;
        if(_timer <= 0)
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
            }
            else
            {
                _bulletDestroySword = false;
            }
        }

        bullet.SetStats(_bulletDestroySword);
    }

    public void WakeUp()
    {
        StartCoroutine(WaitForSleep(_awakenTime));
        enabled = true;
    }

    public void Sleep()
    {
        enabled = false;
    }

    public IEnumerator WaitForSleep(float awakenTime)
    {
        yield return new WaitForSeconds(_awakenTime);
        Sleep();
    }
}
