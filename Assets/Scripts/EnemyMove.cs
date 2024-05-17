using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D RB2D;
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        RB2D.velocity = new Vector2(moveSpeed, 0f);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipSprite();
    }
    void FlipSprite()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(RB2D.velocity.x)), 1f);
    }
}
