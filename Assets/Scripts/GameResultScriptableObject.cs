using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameResultScriptableObject", order = 1)]
public class GameResultScriptableObject : ScriptableObject
{
    public bool isWin;
    public int score;
}
