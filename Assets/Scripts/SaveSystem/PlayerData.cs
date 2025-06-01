using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;

    public PlayerData(string _name, int _score)
    {
        name = _name;
        score = _score;
    }
}
