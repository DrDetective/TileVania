using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] float Lives = 3f;
    [SerializeField] float score = 0f;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
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
    private void Start()
    {
        livesText.text = Lives.ToString();
        scoreText.text = score.ToString();
    }
    public void Addpoints(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString();
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
        livesText.text = Lives.ToString();
    }
    void ResetGSession()
    {
        FindAnyObjectByType<Persist>().ResetPScene();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
