﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour, IInteractableWithSword, ISleeping
{
    [SerializeField] private MonoBehaviour _movementScript;
    [SerializeField] private float _awakenTime;
    [SerializeField] private bool _destroySwordOnInteract;
    [SerializeField] private Color _acid;

    private void Start()
    {
        if (_destroySwordOnInteract)
            GetComponent<SpriteRenderer>().color = _acid;
    }

    public void Interact(Sword sword) 
    {
        if (_destroySwordOnInteract)
        {
            sword.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }

    public void WakeUp()
    {
        if (TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetBool("Awake", true);
        }
        
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
        if (TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetBool("Awake", false);
        }
        Sleep();
    }
}