using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayAlarmClock : MonoBehaviour
{
    [SerializeField] private float _rayDistance;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private PlayerMovement _playerMovement;

    public void OnLadderPassed()
    {
        RaycastHit2D middleHit = Physics2D.Raycast(_playerMovement.MiddleRayOrigin, transform.right, _rayDistance, _mask);
        RaycastHit2D upHit = Physics2D.Raycast(_playerMovement.UpRayOrigin, transform.right, _rayDistance, _mask);
        RaycastHit2D bottomHit = Physics2D.Raycast(_playerMovement.BottomRayOrigin, transform.right, _rayDistance, _mask);

        if (middleHit)
        {
            middleHit.transform.TryGetComponent<ISleeping>(out ISleeping _iSleeping);
            _iSleeping.WakeUp();
        }
        if (upHit)
        {
            upHit.transform.TryGetComponent<ISleeping>(out ISleeping _iSleeping);
            _iSleeping.WakeUp();
        }
        if (bottomHit)
        {
            bottomHit.transform.TryGetComponent<ISleeping>(out ISleeping _iSleeping);
            _iSleeping.WakeUp();
        }
    }
}
