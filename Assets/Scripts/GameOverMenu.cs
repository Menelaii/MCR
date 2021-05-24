using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button _respawn;
    [SerializeField] private KeyCode _respawnKey = KeyCode.R;
    [SerializeField] private Text _text;

    public void Update()
    {
        if (Input.GetKeyDown(_respawnKey))
            OnRespawnButtonClick();
    }

    public void OnPlayerDied()
    {
        gameObject.SetActive(true);
        _text.text = "Or press " + _respawnKey + " button";
        _respawn.onClick.AddListener(OnRespawnButtonClick);
    }

    public void OnRespawnButtonClick()
    {
        _respawn.onClick.RemoveListener(OnRespawnButtonClick);
        SceneManager.LoadScene(0);
    }
}
