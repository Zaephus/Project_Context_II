using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int currentTurn = 1;
    public int currentScore = 0;
    public int currentTiles = 1;

    public TextMeshProUGUI turnText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI tilesText;

    private void Start() {
        Cursor.visible = false;
    }
    void Update()
    {
        turnText.text = "" + currentTurn;
        scoreText.text = "" + currentScore;
        tilesText.text = "" + currentTiles;
    }
}
