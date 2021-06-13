using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private Text _scoreView;
    [SerializeField] private Text _recordView;

    private int _score;

    public int Score => _score;

    private void Start()
    {
        _recordView.text = "Record: " + _stats.Record.ToString();
        _scoreView.text = "Score: " + _score.ToString();
    }

    public void OnLadderPassed()
    {
        _score++;
        _scoreView.text = "Score: " + _score.ToString();
    }

    public void OnPlayerDied()
    {
        _recordView.text = "Record: " + _stats.Record.ToString();
    }
}
