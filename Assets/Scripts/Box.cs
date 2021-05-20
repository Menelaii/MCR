using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private bool _destroyable;
    [SerializeField] private bool _spikesInside;
    [SerializeField] DeathZone _spikes;

    public void Interact()
    {
        if (_destroyable == false)
            return;

        if(_spikesInside)
            Instantiate(_spikes, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
