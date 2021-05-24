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
    [SerializeField] private AbilityCooldownView _abilityCooldownView;

    public UnityAction OnLadderPassed;
    public UnityAction OnPlayerDied;

    private void OnEnable()
    {
        OnPlayerDied += _menu.OnPlayerDied;
        OnPlayerDied += _scoreView.OnPlayerDied;
        OnPlayerDied += _abilityCooldownView.OnPlayerDied;
        OnLadderPassed += _camera.OnLadderPassed;
        OnLadderPassed += _generator.OnLadderPassed;
        OnLadderPassed += _scoreView.OnLadderPassed;
    }

    private void OnDisable()
    {
        OnPlayerDied -= _menu.OnPlayerDied;
        OnPlayerDied -= _scoreView.OnPlayerDied;
        OnPlayerDied -= _abilityCooldownView.OnPlayerDied;
        OnLadderPassed -= _camera.OnLadderPassed;
        OnLadderPassed -= _generator.OnLadderPassed;
        OnLadderPassed -= _scoreView.OnLadderPassed;
    }

    public void Die()
    {
        OnPlayerDied?.Invoke();
        Destroy(gameObject);
    }
}
