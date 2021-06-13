using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _destroyable;
    [SerializeField] private int _touchCountToDestroy;
    [SerializeField] private bool _spikesInside;
    [SerializeField] private bool _coinInside;

    [SerializeField] private DeathZone _spikes;
    [SerializeField] private Coin _coin;

    private int _touchCount;

    public void Interact()
    {
        TryToDestroy();
    }

    public void Interact(Sword sword)
    {
        TryToDestroy();
    }

    private void TryToDestroy()
    {
        _touchCount++;
        if (_destroyable == false || _touchCount < _touchCountToDestroy)
            return;

        Destroy(gameObject);
        SpawnItem();
    }

    private void SpawnItem()
    {
        if (_spikesInside)
            Instantiate(_spikes, transform.position, Quaternion.identity);
        else if (_coinInside)
            Instantiate(_coin, transform.position, Quaternion.identity);
    }
}
