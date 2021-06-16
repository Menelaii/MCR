using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private int _difficulty;
    [SerializeField] private int _type;
    [SerializeField] private bool _isLadderOnRight;
    [SerializeField] private bool _isBonus;
    [SerializeField] private bool _needSword;

    public bool IsLadderOnRight => _isLadderOnRight;
    public int Difficulty => _difficulty;
    public bool NeedSword => _needSword;
    public bool IsBonus => _isBonus;
    public int Type => _type;
}
