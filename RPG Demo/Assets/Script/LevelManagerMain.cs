using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerMain : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Level1");
        Screen.SetResolution(1920, 1080, true);
    }

    public void appExit()
    {
        Application.Quit();
    }
}
