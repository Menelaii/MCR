using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private Text _text;

    private void Start()
    {
        _text.text = _stats.Coins.ToString();
    }

    public void OnCoinTaken()
    {
        gameObject.SetActive(true);
        _text.text = _stats.Coins.ToString();
    }
}
