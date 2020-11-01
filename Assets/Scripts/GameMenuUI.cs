using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenuUI : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject menuPanel;
    public GameObject defeatMenu;

    public TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        //Se actualiza el texto de la puntuación
        scoreText.SetText("Coins: " + GameManager.score);
    }
}
