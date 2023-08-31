using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultMenu : MonoBehaviour
{
    public GameResultScriptableObject gameResult;
    public Text resultTextUI;
    public Text scoreTextUI;

    void Start()
    {
        resultTextUI.text = gameResult.isWin ? "You win!" : "The end!";
        scoreTextUI.text = gameResult.score.ToString();
    }
}
