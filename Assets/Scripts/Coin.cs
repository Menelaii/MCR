using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractableWithTouch
{
    public void Interact()
    {
        Destroy(gameObject);
    }

    public void Interact(Sword sword) { }
}
