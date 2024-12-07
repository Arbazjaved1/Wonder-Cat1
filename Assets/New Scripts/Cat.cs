using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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

    public int count;
    public TextMeshProUGUI countText;

    private Transform currentPlatform;
    private Vector3 platformLastPosition;

    private bool jumpheld = false;

    public int totalCoins; // Total coins collected across all scenes (persistent)
    public int currentSceneCoins; // Coins collected in the current scene
    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Load total coins from PlayerPrefs (persistent storage)
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        // Initialize current scene coins to zero
        currentSceneCoins = 0;

        UpdateCountUI();
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

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();

    //    audiomanager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();

    //    // Dynamically load saved coins at the beginning of every scene
    //    currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    //    UpdateCountUI();
    //}

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
        if (other.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);

            audiomanager?.PlaySFX(audiomanager.coins);

            // Increment current scene coins and total coins
            currentSceneCoins++;
            totalCoins++;

            // Save total coins persistently
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.Save();

            UpdateCountUI();
        }
    }

    private void UpdateCountUI()
    {
        // Update the UI with total coins
        countText.text = $" {totalCoins}";
    }
    public void ResetSceneCoins()
    {
        // Subtract current scene coins from total coins
        totalCoins -= currentSceneCoins;

        // Save updated total coins persistently
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();

        // Reset current scene coins
        currentSceneCoins = 0;

        Debug.Log("Coins reset to zero for the current scene.");
        UpdateCountUI();
    }
    public void RetryButton()
    {
        ResetSceneCoins(); // Reset coins for the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    //public void startlevel()
    //{
    //    count = 0;
    //    count = SaveSystem.Load("Pickup", 0);
    //    setcountText();
    //}

}