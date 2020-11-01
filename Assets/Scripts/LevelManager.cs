using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject menuPanel;

    public TMP_Text scoreText;

    private void Start()
    {
        //No se empieza en menús (No se muestra cursor)
        SceneController.inMenu = false;

        //Se reinician las monedas
        GameManager.score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //Se actualiza el texto de la puntuación
        scoreText.SetText("Coins: " + GameManager.score);

        //Si se pulsa ESCAPe, SE PAUSA
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Se activa o desactiva el menú según su estado actual
            menuPanel.SetActive(!menuPanel.activeSelf);

            //Se pausa el juego
            SceneController.PauseGame(); 
        }
    }

    //Misma fucnión que en el scene controller pero no estática para poderse llamara des de los botones de la UI
    public void PauseGame()
    {
        //Si el juego está pausado se despausa, sinó se pausa
        if (GameManager.isGamePaused)
        {
            //Se despausa el juego
            GameManager.isGamePaused = false;

            //No se está en menús
            SceneController.inMenu = false;

            //La escala de tiempo pasa a ser 1, la normal por defecto
            Time.timeScale = 1;
        }
        else
        {
            //Se pausa el juego
            GameManager.isGamePaused = true;

            //No se entra en menús
            SceneController.inMenu = true;

            //La escala de tiempo pasa a ser 0, el tiempo no corre
            Time.timeScale = 0;
        }
    }
}
