using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerFinish : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SceneLoadWithDelay(0, 5));
        Screen.SetResolution(1920, 1080, true);
    }


    IEnumerator SceneLoadWithDelay(int sceneNum, int numSeconds)
{
    yield return new WaitForSeconds(numSeconds);

    SceneManager.LoadScene(sceneNum);
}
}
