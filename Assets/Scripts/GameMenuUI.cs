using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenuUI : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerManager playerManager;
    public LevelManager levelManager;

    public GameObject menuPanel;

    //Menú de derrota
    public GameObject defeatMenu;
    public TMP_Text defeatMenuCoins;
    public Image defeatMenuScroll;
    public Image defeatMenuSpellbook;
    public Image defeatMenuCauldron;

    //Menú de victoria
    public GameObject victoryMenu;
    public TMP_Text victoryMenuCoins;
    public Image victoryMenuScroll;
    public Image victoryMenuSpellbook;
    public Image victoryMenuCauldron;

    public Image scrollImage;
    public Image spellbookImage;
    public Image cauldronImage;

    public TMP_Text scoreText;
    public Slider health;

    // Update is called once per frame
    void Update()
    {
        //Se actualiza el texto de la puntuación
        scoreText.SetText(GameManager.score.ToString());

        //Se actualiza la barra de vida
        health.value = playerManager.currentHealth;

        //Si termina el juego
        if (GameManager.isGameOver)
        {
            //Se asigna el total de monedas al objeto del menú final
            defeatMenuCoins.SetText(GameManager.score.ToString());

            //Se muestran o no los items recolectados
            if(!levelManager.items.scroll)
            {
                defeatMenuScroll.color = new Color(0, 0, 0, 0.3f);
            }
            if (!levelManager.items.spellbook)
            {
                defeatMenuSpellbook.color = new Color (0, 0, 0, 0.3f);
            }
            if (!levelManager.items.cauldron)
            {
                defeatMenuCauldron.color = new Color(0, 0, 0, 0.3f);
            }
        }

        //Si se completa el juego
        if (GameManager.isGameCompleted)
        {
            //Se asigna el total de monedas al objeto del menú final
            victoryMenuCoins.SetText(GameManager.score.ToString());

            //Se muestran o no los items recolectados
            if (!levelManager.items.scroll)
            {
                victoryMenuScroll.color = new Color(0, 0, 0, 0.3f);
            }
            if (!levelManager.items.spellbook)
            {
                victoryMenuSpellbook.color = new Color(0, 0, 0, 0.3f);
            }
            if (!levelManager.items.cauldron)
            {
                victoryMenuCauldron.color = new Color(0, 0, 0, 0.3f);
            }
        }
    }
}
