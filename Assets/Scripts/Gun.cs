using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    Rigidbody2D RB2D;
    PlayerMove player;
    float xSpeed;
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMove>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        RB2D.velocity = new Vector2 (xSpeed, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
