using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
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
    private int currentSceneCoins; // Coins collected in the current session (not saved until level completion)

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Load total coins from PlayerPrefs (persistent storage)
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        // Initialize current scene coins to zero for this session
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

            // Increment current scene coins (but not saved yet)
            currentSceneCoins++;

            UpdateCountUI();
        }
    }

    private void UpdateCountUI()
    {
        // Update the UI with total coins
        countText.text = $" {totalCoins + currentSceneCoins}";
    }

    // Save coins when the level is completed
    public void SaveCoinsOnLevelComplete()
    {
        totalCoins += currentSceneCoins; // Add the current scene coins to total
        PlayerPrefs.SetInt("TotalCoins", totalCoins); // Save updated total coins
        PlayerPrefs.Save();

        Debug.Log("Coins saved for the level.");
    }

    // This method resets scene-specific coins (for when retrying or starting a new scene)
    public void ResetSceneCoins()
    {
        currentSceneCoins = 0;
        UpdateCountUI();
    }
    public void RetryLevel()
    {
        ResetSceneCoins(); // Reset the coins collected in the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene to retry
        Time.timeScale = 1f;
    }
}
