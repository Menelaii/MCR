using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _text;

    private int _score;

    public void OnLadderPassed()
    {
        _score++;
        _text.text = _score.ToString();
    }

    public void OnPlayerDied()
    {
        Image image = gameObject.GetComponent<Image>();

        if(_score > 10)
            image.color = Color.green;
        else
            image.color = Color.red;
    }
}
