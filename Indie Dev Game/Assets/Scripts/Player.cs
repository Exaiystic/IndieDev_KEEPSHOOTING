using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public int extraJumps;
    public float dashSpeed;
    public int extraDashes;
    public float startDashTime;
    public float dashHeight;
    public float invincibilityTime;
    public LayerMask ground;
    public int startHealth;
    public ParticleSystem jumpPS;
    public Color hitColour;

    private Rigidbody2D rb;
    private InputActions inputActions;
    private Collider2D col;
    private float dashTime;
    private int currentHealth;
    private GameObject gameManager;
    private GameManager manager;
    private bool isGrounded;
    private int jumpsLeft;
    private bool isDashing;
    private bool isInvincible;
    private int dashesLeft;
    private int dashDirection;
    private SpriteRenderer sr;
    private Color startColour;
    private float movementInput;

    private void Awake()
    {
        inputActions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputActions.Player.Jump.performed += _ => Jump();
        inputActions.Player.Dash.performed += _ => Dash();
        dashTime = startDashTime;
        currentHealth = startHealth;
        gameManager = GameObject.Find("Game Manager");
        manager = gameManager.GetComponent<GameManager>();
        jumpsLeft = extraJumps;
        dashesLeft = extraDashes;
        sr = GetComponent<SpriteRenderer>();
        startColour = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = inputActions.Player.Movement.ReadValue<float>();
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * moveSpeed * Time.deltaTime;
        transform.position = currentPosition;

        if (IsGrounded())
        {
            jumpsLeft = extraJumps;
            dashesLeft = extraDashes;
        }

        if (isDashing)
        {
            if (dashTime <= 0)
            {
                dashDirection = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                isDashing = false;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (dashDirection == 1)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(new Vector2(0, dashHeight), ForceMode2D.Impulse);
                }
                else if (dashDirection == 2)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(new Vector2(0, dashHeight), ForceMode2D.Impulse);
                }
            }
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x;
        bottomRight.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRight, ground);
    }
    
    private void Jump()
    {
        jumpPS.Play();
        
        if (jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }
    
    private void Dash()
    {
        if (!isDashing)
        {
            if (dashesLeft > 0)
            {
                isDashing = true;
                dashesLeft--;

                if (movementInput > 0)
                {
                    dashDirection = 1;
                }
                else if (movementInput < 0)
                {
                    dashDirection = 2;
                }
            }
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage()
    {
        if (!isInvincible)
        {
            currentHealth--;
            StartCoroutine(Invincible());
        }
        //Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual IEnumerator Invincible()
    {
        isInvincible = true;
        sr.color = hitColour;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
        sr.color = startColour;
    }
    public void Die()
    {
        Destroy(gameObject);
        manager.Lose();
    }
}
