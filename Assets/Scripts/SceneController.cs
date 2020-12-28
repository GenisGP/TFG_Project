﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static bool inGame;                          //Si se está en la pantalla de juego
    public static bool inMenu;                          //Si se está navegando por menús

    private GameMenuUI menuManager;

    // Update is called once per frame
    void Update()
    {
        //Si se está en menús se uestra el cursor
        if (/*inMenu || */(SceneManager.GetActiveScene().name == "MainMenu"))
        {
            Cursor.visible = true;
            Time.timeScale = 1;
        }
        //Sinó, se oculta el cursor
        else
        {
            //Cursor.visible = false;
        }
    }
    //Carga de escena
    public void LoadScene(string sceneN)
    {
        SceneManager.LoadScene(sceneN);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelScene(string sceneN)
    {
        SceneManager.LoadScene(sceneN);

        //No se está en menús
        inMenu = false;
    }

    //Reinicia la escena actual
    public void RestartScene()
    {
        GameManager.RestartScene();
    }

    //Carga de escena asíncrona
    public void AsyncLoad()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    //Salir del juego
    public void Exit()
    {
        Application.Quit();
    }

    public static void PauseGame()
    {
        //Si el juego está pausado se despausa, sinó se pausa
        if (GameManager.isGamePaused)
        {
            //Se despausa el juego
            GameManager.isGamePaused = false;

            //No se está en menús
            inMenu = false;

            //La escala de tiempo pasa a ser 1, la normal por defecto
            Time.timeScale = 1;
        }
        else
        {
            //Se pausa el juego
            GameManager.isGamePaused = true;

            //No se entra en menús
            inMenu = true;

            //La escala de tiempo pasa a ser 0, el tiempo no corre
            Time.timeScale = 0;
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        //Se carga la escena en el fondo minetras la escena actual sigue activa.
        //Esto es particularmente bueno para crear escenas de carga
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game_Scene");

        //Espera hasta que la escena asíncrona está completamente cargada
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}