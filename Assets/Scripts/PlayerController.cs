﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private  PlayerCombat playerCombat;
    private  PlayerManager playerManager;

    public float speed;
    public float jumpForce;
    private float movement;

    public bool isGrounded;                 //Si está en el suelo
    private bool isJumping;                 //Si está saltando
    private bool startJump;                 //Para detectar en Input del salto i aplicarlo en el FixedUpdate sin perderlo

    [SerializeField] private Transform groundCheck;     //Para detectar si está tocando el suelo
    [SerializeField] private Transform groundCheckL;    //Para detectar si está tocando el suelo en la base izquierda
    [SerializeField] private Transform groundCheckR;    //Para detectar si está tocando el suelo en la base derecha

    float originalXScale;					//Esacala original en el eje X
    int direction = 1;						//Direccióm a la que mira el jugador (1 -> Derecha, -1-> Izquierda)

    private Rigidbody2D rb2d;
    public Animator anim;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        rb2d= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        playerManager = GetComponent<PlayerManager>();

        //Se almacena la escala x original del personaje
        originalXScale = transform.localScale.x;

        //Cuando se inicia el nivel se quita la pausa (para no hacer el return en el Update)
        GameManager.isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el Game Manager dice que se acabó el juego o está en pausa, salir
        if (GameManager.isGameOver || GameManager.isGamePaused)
        {
            movement = 0f;
            return;
        }

        //Si el jugador no está atacando se podrá mover
        if (playerCombat.isRecovered && !playerCombat.isAttacking && !GameManager.isGameCompleted)
        {
            movement = Input.GetAxis("Horizontal");
        }
        else
        {
            movement = 0f;
        }

        //Se pasa el valor absoluto de la velocidad para pasar solo valores positivos
        anim.SetFloat("speed", Mathf.Abs(movement));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", isJumping);


        //Si el signo del movimiento del Input y la dirección no coindicen, se da la vuelta al personaje y el personage no está siendo golpeado
        if (movement * direction < 0f && playerCombat.isRecovered)
            FlipCharacterDirection();

        //Si se pulsa el botón de saltar, está en el suelo y no está siendo golpeado
        if (Input.GetButtonDown("Jump") && isGrounded && playerCombat.isRecovered)
        {
            startJump = true;
            isJumping = true;
            playerCombat.isAttacking = false;

            audioManager.PlaySound("Jump");
        }
    }
    private void FixedUpdate()
    {
        //Si el linecast va en la trayectoria desde las posición del jugador a la del groundCheck y choca con la layer Ground
        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
        }

        //Movimiento
        rb2d.velocity = new Vector2(movement * speed, rb2d.velocity.y);

        if (startJump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.Play("Jump");
            startJump = false;
            isJumping = true;
        }
    }

    void FlipCharacterDirection()
    {
        //Gira el personage dando la vuelta a la dirección
        direction *= -1;

        //Escala actual
        Vector3 scale = transform.localScale;

        //Establece la escala X para ser la original por la dirección
        scale.x = originalXScale * direction;

        //Aplica la nueva escala
        transform.localScale = scale;
    }

    //Sonidos de andar y caer al suelo llamados en eventos de las animcaciones
    void FootSteep()
    {
        audioManager.PlaySound("FootStep");
    }
    void JumpLand()
    {
        audioManager.PlaySound("JumpLand");
    }
}
