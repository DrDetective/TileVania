using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 DKick = new Vector2(10f,10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun; 
    Animator animator;
    Vector2 moveInput;
    Rigidbody2D RB2D;
    //CapsuleCollider2D CCollider;
    BoxCollider2D BCollider;
    float gravityStart;
    //float gravityOnLadder;
    bool alive = true;
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //CCollider = GetComponent<CapsuleCollider2D>();
        BCollider = GetComponent<BoxCollider2D>();
        gravityStart = RB2D.gravityScale;
    }

    void Update()
    {
        if (!alive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Died();     
    }
    void OnFire(InputValue inputValue)
    {
        if(!alive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnMove(InputValue inputValue)
    {
        if (!alive) { return; }
        moveInput = inputValue.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue inputValue)
    {
        if (!alive) { return; }
        if (!BCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        else if (inputValue.isPressed)
        {
            RB2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 vectorVelocity = new Vector2(moveInput.x * moveSpeed, RB2D.velocity.y); 
        RB2D.velocity = vectorVelocity;
        bool PlHorizontalSpeed = Mathf.Abs(RB2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", PlHorizontalSpeed);
    }
    void FlipSprite()
    {
        bool PlHorizontalSpeed = Mathf.Abs(RB2D.velocity.x) > Mathf.Epsilon;
        if(PlHorizontalSpeed) { transform.localScale = new Vector2(Mathf.Sign(RB2D.velocity.x), 1f); }
        
    }
    void ClimbLadder()
    {
        if (!BCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            RB2D.gravityScale = gravityStart;
            animator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(RB2D.velocity.x, moveInput.y * climbSpeed);
        RB2D.velocity = climbVelocity;
        RB2D.gravityScale = 0f;

        bool PlVerticalSpeed = Mathf.Abs(RB2D.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", PlVerticalSpeed);
    }
    void Died()
    {
        if (BCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            alive = false;
            animator.SetTrigger("Died");
            RB2D.velocity = DKick;
            FindObjectOfType<GameSession>().PlayerDead();
        }
    }
}
