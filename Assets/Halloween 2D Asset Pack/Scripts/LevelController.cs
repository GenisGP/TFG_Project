using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour {


	public void LoadScene (string sceneN) {
		SceneManager.LoadScene (sceneN);
	}
	//Load empty scene
	
	  public void ASceneLoad(string sceneN)
    {
        SceneManager.LoadScene(sceneN,LoadSceneMode.Single);
    }

    public void AsyncLoad ()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game_Scene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}