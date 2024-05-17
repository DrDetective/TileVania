using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] float Lives = 3f;
    void Awake()
    {
        int GSs = FindObjectsOfType<GameSession>().Length;
        if (GSs > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerDead()
    {
        if (Lives > 1)
        {
            TakeLive();
        }
        else
        {
            ResetGSession();
        }
    }
    void TakeLive()
    {
        Lives--;
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }
    void ResetGSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
