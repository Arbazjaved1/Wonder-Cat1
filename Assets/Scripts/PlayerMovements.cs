using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;
public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 40f;

    public float JumpHeight = 10f;

    private Animator anim;

    private bool grounded;

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
        //Vector2 movedirection = moveactiontouse.action.ReadValue<Vector2>();
        //rb.velocity = new Vector2(movedirection.x * speed, rb.velocity.y);
        ////float horizontalinput = Input.GetAxis("Horizontal");
        //if (movedirection.x > 0.01f)
        //    transform.localScale = Vector3.one;
        //else if (movedirection.x < -0.01f)
        //    transform.localScale = new Vector3(-1, 1, 1);

        //// Set animator parameters
        //anim.SetBool("Run", movedirection.x != 0);
        //anim.SetBool("Grounded", grounded);
        //if (jumpheld && grounded)
        //    Jump();






        float horizontalinput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(horizontalinput * speed, rb.velocity.y);
        //flip player when moving left-right
        if (horizontalinput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalinput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        if (Input.GetKeyDown(KeyCode.Space))
            jumpheld = true;
        if (Input.GetKeyUp(KeyCode.Space))
            jumpheld = false;

        if (jumpheld && grounded)
            Jump();

        //set animator paramenters
        anim.SetBool("Run", horizontalinput != 0);
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
        if (grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            anim.SetTrigger("Jump");
            grounded = false;
        }

    }
    private void Awake()
    {
        //refrence of regidbody and animator
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
       //collectionsoundeffect = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - platformLastPosition;
            transform.position += platformMovement;
            platformLastPosition = currentPlatform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
            currentPlatform = null;

        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
            currentPlatform = collision.transform;
            platformLastPosition = currentPlatform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentPlatform)
        {
            currentPlatform = null;
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
