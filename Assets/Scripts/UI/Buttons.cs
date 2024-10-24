using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void ToStartScreen()
    {
        SceneManager.LoadScene("inicio");
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
    }

    public void ToGameScreen()
    {
        SceneManager.LoadScene("noGame");
    }
    public void ToDialogueScene()
    {
        SceneManager.LoadScene("dialogo2");
    }
    public void ToDialogueScene2()
    {
        SceneManager.LoadScene("dialogo");
    }

    public void Pause()
    {
        PauseMenu.isPaused = true;
    }

    public void Resume()
    {
        PauseMenu.isPaused = false;
    }
}
