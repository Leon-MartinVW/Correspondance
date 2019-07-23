using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelContoller : MonoBehaviour
{
    private float timeToLoad = 1f;

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOver());
    }

    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(timeToLoad);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
