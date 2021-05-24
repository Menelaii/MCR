using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button _respawn;

    public void OnPlayerDied()
    {
        gameObject.SetActive(true);
        _respawn.onClick.AddListener(OnRespawnButtonClick);
    }

    public void OnRespawnButtonClick()
    {
        _respawn.onClick.RemoveListener(OnRespawnButtonClick);
        SceneManager.LoadScene(0);
    }
}
