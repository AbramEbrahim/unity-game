using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 20f;
    public float jumpForce = 20f;
    public float groundCheckRadius = 0.1f; 
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

   
    [SerializeField]private AudioClip juuumb;
    [SerializeField]private AudioClip walk;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(transform.position + Vector3.down * groundCheckRadius, groundCheckRadius, whatIsGround);

        
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            SoundManager.instance.PlaySound(walk);
        }
        animator.SetBool("isMoving", Mathf.Abs(moveHorizontal) > 0);

        if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            SoundManager.instance.PlaySound(juuumb);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
           
            animator.SetBool("isJumping",true);
        }
        else 
        {
            animator.SetBool("isJumping", false);
        }

        

        


    }
 


}
