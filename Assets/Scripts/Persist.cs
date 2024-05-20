using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour
{
    void Awake()
    {
        int persistScene = FindObjectsOfType<Persist>().Length;
        if (persistScene > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetPScene()
    {
        Destroy(gameObject);
    }
}
