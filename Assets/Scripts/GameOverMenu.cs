using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private KeyCode _respawnKey;
    [SerializeField] private int _coinsForRespawn;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _respawn;
    [SerializeField] private Text _respawnKeyMessage;
    [SerializeField] private Player _player;

    public void Update()
    {
        if (Input.GetKeyDown(_respawnKey))
        {
            OnRestartButtonClick();
        }
    }

    public void OnPlayerDied()
    {
        gameObject.SetActive(true);
        _respawnKeyMessage.text = "Or press " + _respawnKey + " button";

        if (_player.Stats.Coins < _coinsForRespawn)
            _respawn.interactable = false;

        _restart.onClick.AddListener(OnRestartButtonClick);
        _respawn.onClick.AddListener(OnRespawnButtonClick);
    }

    private void OnRestartButtonClick()
    {
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _respawn.onClick.RemoveListener(OnRespawnButtonClick);
        SceneManager.LoadScene(0);
    }

    private void OnRespawnButtonClick()
    {
        if (_respawn.interactable == false)
            return;

        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _respawn.onClick.RemoveListener(OnRespawnButtonClick);
        
        RespawnForCoins();
    }

    private void RespawnForCoins()
    {
        _player.RespawnForCoins(_coinsForRespawn);
        gameObject.SetActive(false);
    }
}
