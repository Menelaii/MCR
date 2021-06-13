using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private LevelGenerator _generator;
    [SerializeField] private GameOverMenu _menu;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private CoinsView _coinsView;
    [SerializeField] private AbilityCooldownView _abilityCooldownView;
    [SerializeField] private PlayerStats _stats;

    public UnityAction OnLadderPassed;
    public UnityAction OnPlayerDied;
    public UnityAction OnCoinTaken;

    private void OnEnable()
    {
        OnPlayerDied += _menu.OnPlayerDied;
        OnPlayerDied += _scoreView.OnPlayerDied;
        OnPlayerDied += _abilityCooldownView.OnPlayerDied;
        OnCoinTaken += _coinsView.OnCoinTaken;
        OnLadderPassed += _camera.OnLadderPassed;
        OnLadderPassed += _generator.OnLadderPassed;
        OnLadderPassed += _scoreView.OnLadderPassed;
    }

    private void OnDisable()
    {
        OnPlayerDied -= _menu.OnPlayerDied;
        OnPlayerDied -= _scoreView.OnPlayerDied;
        OnPlayerDied -= _abilityCooldownView.OnPlayerDied;
        OnCoinTaken -= _coinsView.OnCoinTaken;
        OnLadderPassed -= _camera.OnLadderPassed;
        OnLadderPassed -= _generator.OnLadderPassed;
        OnLadderPassed -= _scoreView.OnLadderPassed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<Coin>(out Coin coin))
        {
            TakeCoin(coin);
        }
    }

    public void Die()
    {
        TryUpdateRecord();
        OnPlayerDied?.Invoke();
        Destroy(gameObject);
    }

    public void TryUpdateRecord()
    {
        if(_scoreView.Score > _stats.Record)
            _stats.Record = _scoreView.Score;
    }

    public void TakeCoin(Coin coin)
    {
        coin.Interact();
        _stats.Coins++;
        OnCoinTaken?.Invoke();
    }
}
