using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableWithTouch
{
    void Interact();
}

public interface IInteractableWithSword
{
    void Interact(Sword sword);
}

public interface ISleeping
{
    void WakeUp();
    void Sleep();
    IEnumerator WaitForSleep(float awakenTime);
}