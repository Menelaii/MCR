using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 100;
    [SerializeField] private float _jumpVelocity = 4.4f;
    [SerializeField] private float _rayDistance = 0.1f;
    [SerializeField] private float _bottomRayHeight = 0.02f;
    [SerializeField] private float _upRayHeight = 1.2f;
    [SerializeField] private float _middleRayHeight = 0.6f;
    [SerializeField] private float _jumpRememberTime = 0.25f;
    [SerializeField] private float _groundedRemeberTime = 0.1f;
    [SerializeField] private float _checkGroundCircleRadius = 0.01f;
    [SerializeField] private LayerMask _obstacles;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    private float _moveDirectionX;
    private float _skinWidth = 0.015f;
    private float _originX;
    private float _jumpRemember;
    private float _groundedRemember;

    public Vector2 BottomRayOrigin => new Vector2(_originX, _collider.bounds.min.y + _bottomRayHeight);
    public Vector2 MiddleRayOrigin => new Vector2(_originX, _collider.bounds.min.y + _middleRayHeight);
    public Vector2 UpRayOrigin => new Vector2(_originX, _collider.bounds.min.y + _upRayHeight);

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

        RaycastHit2D middleHit = Physics2D.Raycast(MiddleRayOrigin, transform.right, _rayDistance, _obstacles);
        RaycastHit2D bottomHit = Physics2D.Raycast(BottomRayOrigin, transform.right, _rayDistance, _obstacles);
        RaycastHit2D upHit = Physics2D.Raycast(UpRayOrigin, transform.right, _rayDistance, _obstacles);

        if (bottomHit || upHit || middleHit)
        {
            if (upHit)
                TryInteract(upHit);
            else if (bottomHit)
                TryInteract(bottomHit);
            else
                TryInteract(middleHit);

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
        Gizmos.DrawLine(BottomRayOrigin, BottomRayOrigin + (Vector2)transform.right * _rayDistance);
        Gizmos.DrawLine(UpRayOrigin, UpRayOrigin + (Vector2)transform.right * _rayDistance);
        Gizmos.DrawLine(MiddleRayOrigin, MiddleRayOrigin + (Vector2)transform.right * _rayDistance);
        Vector2 origin = new Vector2(transform.position.x, _collider.bounds.min.y);
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
        Vector2 origin = new Vector2(transform.position.x, _collider.bounds.min.y);
        bool grounded = Physics2D.OverlapCircle(origin, _checkGroundCircleRadius, _ground);

        return grounded;
    }

    private void TryInteract(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent<IInteractableWithTouch>(out IInteractableWithTouch IInteractable))
            IInteractable.Interact();
    }
}
