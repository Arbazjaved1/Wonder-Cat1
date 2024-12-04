using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;

public class Cat : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 40f;
    public float JumpHeight = 10f;

    private Animator anim;
    private bool grounded;
    private bool falling = false; // Track if the player is falling

    private AudioManager audiomanager;

    public InputActionReference moveactiontouse;
    public LevelManager levelManager;

    public int count;
    public TextMeshProUGUI countText;

    private Transform currentPlatform;
    private Vector3 platformLastPosition;

    private bool jumpheld = false;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        count = 0;
        setcountText();
        startlevel();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movedirection = moveactiontouse.action.ReadValue<Vector2>();
        rb.velocity = new Vector2(movedirection.x * speed, rb.velocity.y);

        // Flip player when moving left-right
        if (movedirection.x > 0.01f)
        {
            // Preserve original y and z scales
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (movedirection.x < -0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Handle jumping
        if (jumpheld && grounded && !falling)
            Jump();

        // Set animator parameters
        anim.SetBool("Run", movedirection.x != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void OnEnable()
    {
        moveactiontouse.action.Enable();
    }

    private void OnDisable()
    {
        moveactiontouse.action.Disable();
    }

    public void Jump()
    {
        if (grounded && !falling) // Prevent jumping while falling
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            anim.SetTrigger("Jump");
            grounded = false;
        }
    }

    private void Awake()
    {
        // Reference Rigidbody and Animator
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - platformLastPosition;
            transform.position += platformMovement;
            platformLastPosition = currentPlatform.position;
        }

        // Detect if player is falling due to velocity
        if (rb.velocity.y < -0.1f && !grounded)
        {
            falling = true; // Player is falling based on downward velocity
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            falling = false; // Reset falling state when touching the ground
            currentPlatform = null;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
            falling = false; // Reset falling state when touching a platform
            currentPlatform = collision.transform;
            platformLastPosition = currentPlatform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentPlatform || collision.gameObject.CompareTag("Ground"))
        {
            currentPlatform = null;
            grounded = false;
            falling = true; // Player is now falling
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            audiomanager.PlaySFX(audiomanager.coins);
            count = count + 1;
            setcountText();
        }
    }

    public void setcountText()
    {
        countText.text = "  " + count.ToString();
    }

    public void startlevel()
    {
        count = 0;
        count = SaveSystem.Load("Pickup", 0);
        setcountText();
    }

    public void AddCoins(int amount)
    {
        count += amount;
        setcountText();
    }
}