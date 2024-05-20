using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int pointsToAdd = 100;
    bool collected = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !collected)
        {
            collected = true;
            FindAnyObjectByType<GameSession>().Addpoints(pointsToAdd);
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
