using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractableWithSword, ISleeping
{
    [SerializeField] private WayPointMovement _movementScript;
    [SerializeField] private float _awakenTime;
    public void Interact(Sword sword) 
    {
        Destroy(gameObject);
    }
    public void WakeUp()
    {
        StartCoroutine(WaitForSleep(_awakenTime));
        _movementScript.enabled = true;
    }
    public void Sleep()
    {
        _movementScript.enabled = false;
    }
    public IEnumerator WaitForSleep(float awakenTime)
    {
        yield return new WaitForSeconds(_awakenTime);
        Sleep();
    }
}
