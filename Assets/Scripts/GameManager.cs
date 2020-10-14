using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton: Se crea una referencia estática a el mismo para asegurar que solo
    //habrá uno. Los otros scripts acceden a este a tarvés de sus métodos públicos estáticos
    static GameManager current;

    bool isGameOver;							//Si el juego está finalizado

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

    // Update is called once per frame
    void Update()
    {
        //Si el juego está finalizado, sal
        if (isGameOver)
            return;
    }

    public static bool IsGameOver()
    {
        //Si no hay ningús Game Manager presente devuelve falso
        if (current == null)
            return false;

        //Return the state of the game
        return current.isGameOver;
    }

    public static void PlayerDied()
    {
        //Si no hay ningún Game Manager presente devuelve falso
        if (current == null)
            return;

        //Llama al método RestartScene() después de un delay
        current.Invoke("RestartScene", 2f);
    }

    void RestartScene()
    {
        //Clear the current list of orbs
        //orbs.Clear();

        //Play the scene restart audio
       // AudioManager.PlaySceneRestartAudio();

        //Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("escena cargada");
    }
}
