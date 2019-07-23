using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement
    public float speed;
    public float jumpforce;
    private float moveInput;
    private int extraJumps;
    public int numJumps;
    private bool facingRight = true;

    //ladder
    private float inputVertical;
    public float dist;
    public LayerMask whatIsLadder;
    bool isClimbing;
    public float ladderSpeed;
    public float normalGravity;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //living
    bool isAlive = true;
    public Vector2 deathKick= new Vector2(25f,25f);
    private CapsuleCollider2D myBodyCollider;

    private void Start()
    {
        extraJumps = numJumps;
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if (!isAlive) { return; }
        JumpAndRunAndClimb();

    }

    private void JumpAndRunAndClimb()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal"); //move axis equal to 1 when right pressed and equal to -1 when left pressed
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        //ladder - using vertical ray
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, dist, whatIsLadder);//startpos, direction, length

        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isClimbing = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                isClimbing = false;
            }
        }

        if (isClimbing == true && hitInfo.collider != null)
        {
            inputVertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * ladderSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    void Update()
    {

        if (!isAlive) { return; }
        Die();
        if (isGrounded == true)
        {
            extraJumps = numJumps;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpforce;
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))){
            //can create a death animation here
            /*
             * myAnimator.SetTrigger("Die");
             */
            isAlive=false;
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}
