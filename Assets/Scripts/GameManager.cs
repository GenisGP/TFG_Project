﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton: Se crea una referencia estática a el mismo para asegurar que solo habrá uno. 
    //Los otros scripts acceden a este a tarvés de sus métodos públicos estáticos
    static GameManager current;

    public static bool isGameOver;					    //Si el juego está finalizado
    public static bool isGamePaused = false;            //Si el juego está pausado

    public static int score = 0;                        //Puntuación

    private GameMenuUI menuManager;

    private void Awake()
    {

        //Si existe un Game Manager y no es este...
        if (current != null && current != this)
        {
            //Destruye este y finaliza. Solo puede haber un Game Manager
            Destroy(gameObject);
            return;
        }

        //Asigna este como Game Manager actual
        current = this;

        //Para que este objeto persista entre recargas de escenas
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Si el juego está finalizado, sal
        if (isGameOver)
            return;
    }

    public static void PlayerWin()
    {
        //Si no hay ningún Game Manager presente devuelve falso
        if (current == null)
            return;

        //Llama al método RestartScene() después de un delay
        current.Invoke("ShowVictoryMenu", 2f);
    }

    public static void ScoreIncrease(int num)
    {
        score += num;
    }

    public static void PlayerDied()
    {
        //Si no hay ningún Game Manager presente devuelve falso
        if (current == null)
            return;

        //Llama al método RestartScene() después de un delay
        current.Invoke("ShowDefeatMenu", 2f);
    }

    //Mostrar menú de derrta
    void ShowDefeatMenu()
    {
        //Se busca el objeto que contiene los menus i se asigna a la variable
        menuManager = FindObjectOfType<GameMenuUI>();

        //Mostrar el cursor
        Cursor.visible = true;

        menuManager.defeatMenu.SetActive(true);
    }


    public static void RestartScene()
    {
        //Clear the current list of orbs
        //orbs.Clear();

        //Play the scene restart audio
        // AudioManager.PlaySceneRestartAudio();

        //Reinicia la puntuación
        score = 0;
        
        //No mostrar el cursor
        //Cursor.visible = false;

        //Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /*
public static bool IsGameOver()
{
    //Si no hay ningún Game Manager presente devuelve falso
    if (current == null)
        return false;

    //Return the state of the game
    return current.isGameOver;
}
*/
    /*
    public static bool IsGamePaused()
    {
        //Si no hay ningún Game Manager presente devuelve falso
        if (current == null)
            return false;

        //Devuelve el estado del juego
        return current.isGameOver;
    }
    */
}
