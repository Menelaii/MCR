using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void OnCoinsChanged(int coins)
    {
        gameObject.SetActive(true);
        _text.text = coins.ToString();
    }
}
