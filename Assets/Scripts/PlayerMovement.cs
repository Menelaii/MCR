using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 100;
    [SerializeField] private float _jumpVelocity = 4.4f;
    [SerializeField] private float _rayDistance = 0.1f;
    [SerializeField] private float _bottomRayHeight = 0.1f;
    [SerializeField] private float _upRayHeight = 0.3f;
    [SerializeField] private float _jumpRememberTime = 0.25f;
    [SerializeField] private float _groundedRemeberTime = 0.1f;
    [SerializeField] private float _checkGroundCircleRadius = 0.01f;
    [SerializeField] private LayerMask _obstacles;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BoxCollider2D _collider;

    private float _moveDirectionX;
    private float _skinWidth = 0.015f;
    private float _originX;
    private float _jumpRemember;
    private float _groundedRemember;
    private Vector2 _bottomRayOrigin;
    private Vector2 _upRayOrigin;

    private void Start()
    {
        _moveDirectionX = transform.right.x;
        _collider.bounds.Expand(_skinWidth * -2);
        _originX = _collider.bounds.min.x;
    }

    private void Update()
    {
        if (_moveDirectionX == Vector2.left.x)
            _originX = _collider.bounds.min.x;
        else if (_moveDirectionX == Vector2.right.x)
            _originX = _collider.bounds.max.x;

        _groundedRemember -= Time.deltaTime;
        if (GroundCheck())
        {
            _groundedRemember = _groundedRemeberTime;
        }

        _jumpRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRemember = _jumpRememberTime;
        }

        if (_jumpRemember > 0 && _groundedRemember > 0)
        {
            _jumpRemember = 0;
            _groundedRemember = 0;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpVelocity);
        }

        _bottomRayOrigin = new Vector2(_originX, _collider.bounds.min.y + _bottomRayHeight);
        _upRayOrigin = new Vector2(_originX, _collider.bounds.max.y - _upRayHeight);
        RaycastHit2D bottomHit = Physics2D.Raycast(_bottomRayOrigin, transform.right, _rayDistance, _obstacles);
        RaycastHit2D upHit = Physics2D.Raycast(_upRayOrigin, transform.right, _rayDistance, _obstacles);

        if (bottomHit || upHit)
        {
            if (bottomHit)
                TryInteract(bottomHit);
            if (upHit)
                TryInteract(upHit);

            ChangeMoveDirectionX();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveDirectionX * _speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_bottomRayOrigin, _bottomRayOrigin + (Vector2)transform.right * _rayDistance);
        Gizmos.DrawLine(_upRayOrigin, _upRayOrigin + (Vector2)transform.right * _rayDistance);
        Vector2 origin = new Vector2(transform.position.x, _collider.bounds.min.y);//_originX
        Gizmos.DrawWireSphere(origin, _checkGroundCircleRadius);
    }

    public void ChangeMoveDirectionX()
    {
        if (transform.eulerAngles.y == 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);

        _moveDirectionX = transform.right.x;
    }

    private bool GroundCheck()
    {
        Vector2 origin = new Vector2(transform.position.x, _collider.bounds.min.y);//_originX
        bool grounded = Physics2D.OverlapCircle(origin, _checkGroundCircleRadius, _ground);

        return grounded;
    }

    private void TryInteract(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent<Box>(out Box box))
            box.Interact();
    }
}
