using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenPrevSceneOrExit();
    }

    // back to previous scene or quit
    private void OpenPrevSceneOrExit()
    {
        int buildSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildSceneIndex > 0)
            SceneManager.LoadScene(--buildSceneIndex);
        else
            Application.Quit();
    }
}
