using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private LevelGenerator _generator;
    [SerializeField] private RayAlarmClock _alarmClock;
    [SerializeField] private GameOverMenu _menu;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private CoinsView _coinsView;
    [SerializeField] private AbilityCooldownView _abilityCooldownView;
    [SerializeField] private PlayerStats _stats;

    public UnityAction OnLadderPassed;
    public UnityAction OnPlayerDied;
    public UnityAction<int> OnCoinsChanged;

    public PlayerStats Stats => _stats;

    private void OnEnable()
    {
        OnPlayerDied += _menu.OnPlayerDied;
        OnPlayerDied += _scoreView.OnPlayerDied;
        OnPlayerDied += _abilityCooldownView.OnPlayerDied;
        OnCoinsChanged += _coinsView.OnCoinsChanged;
        OnLadderPassed += _camera.OnLadderPassed;
        OnLadderPassed += _generator.OnLadderPassed;
        OnLadderPassed += _scoreView.OnLadderPassed;
        OnLadderPassed += _alarmClock.OnLadderPassed;

        OnCoinsChanged?.Invoke(_stats.Coins);
    }

    private void OnDisable()
    {
        OnPlayerDied -= _menu.OnPlayerDied;
        OnPlayerDied -= _scoreView.OnPlayerDied;
        OnPlayerDied -= _abilityCooldownView.OnPlayerDied;
        OnCoinsChanged -= _coinsView.OnCoinsChanged;
        OnLadderPassed -= _camera.OnLadderPassed;
        OnLadderPassed -= _generator.OnLadderPassed;
        OnLadderPassed -= _scoreView.OnLadderPassed;
        OnLadderPassed -= _alarmClock.OnLadderPassed;
    }

    public void Die()
    {
        TryUpdateRecord();
        OnPlayerDied?.Invoke();
        gameObject.SetActive(false);
    }

    public void RespawnForCoins(int coinsForRespawn)
    {
        _stats.Coins -= coinsForRespawn;
        OnCoinsChanged?.Invoke(_stats.Coins);
        gameObject.SetActive(true);
    }

    public void TryUpdateRecord()
    {
        if(_scoreView.Score > _stats.Record)
            _stats.Record = _scoreView.Score;
    }

    public void TakeCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        _stats.Coins++;
        OnCoinsChanged?.Invoke(_stats.Coins);
    }
}
