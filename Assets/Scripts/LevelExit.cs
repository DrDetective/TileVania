using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LoadDelay = 2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNLevel());
    }
    IEnumerator LoadNLevel()
    {
        yield return new WaitForSecondsRealtime(LoadDelay);
        int CSIndex = SceneManager.GetActiveScene().buildIndex;
        int NextScene = CSIndex + 1;
        if (NextScene == SceneManager.sceneCountInBuildSettings)
        { NextScene = 0; }
        SceneManager.LoadScene(NextScene);
    }
}
