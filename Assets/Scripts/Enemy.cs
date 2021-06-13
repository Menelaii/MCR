using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    public virtual void Interact() { }

    public virtual void Interact(Sword sword) 
    {
        Destroy(gameObject);
    }
}
