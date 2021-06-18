using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int _coins;
    [SerializeField] private int _record;

    public int Coins 
    { 
        get 
        {
            return _coins;
        } 
        set 
        {
            if (value < 0)
                _coins = 0;
            else
                _coins = value;
        }
    }

    public int Record
    {
        get
        {
            return _record;
        }
        set
        {
            if (value < 0)
                _record = 0;
            else
                _record = value;
        }
    }
}
