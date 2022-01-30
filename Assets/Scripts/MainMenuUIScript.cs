using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIScript : MonoBehaviour
{
    public int sceneMainGame;

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneMainGame);
    }

    public void ExitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
