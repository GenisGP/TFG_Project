using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float movement;

    private bool isGrounded;                //Si está en el suelo
    private bool isJumping;                 //Si está saltando
    private bool startJump;                 //Para detectar en Input del salto i aplicarlo en el FixedUpdate sin perderlo

    [SerializeField]
    private Transform groundCheck;

    float originalXScale;					//Esacala original en el eje X
    int direction = 1;						//Direccióm a la que mira el jugador (1 -> Derecha, -1-> Izquierda)

    private Rigidbody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Se almacena la escala x original del personaje
        originalXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //If the Game Manager says the game is over, exit
        if (GameManager.IsGameOver())
        {
            return;
        }
            
        movement = Input.GetAxis("Horizontal");

        //Se pasa el valor absoluto de la velocidad para pasar solo valores positivos
        anim.SetFloat("speed", Mathf.Abs(movement));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", isJumping);


        //Si el signo del movimiento del Input y la dirección no coindicen, se da la vuelta al personaje
        if (movement * direction < 0f)
            FlipCharacterDirection();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            startJump = true;
            isJumping = true;
        }
    }
    private void FixedUpdate()
    {
        //Si el linecast va en la trayectoria desde las posición del jugador a la del groundCheck y choca con la layer Ground
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
            isJumping = false;
            Debug.Log("Grounded");
        }
        else
        {
            isGrounded = false;
            Debug.Log("No Grounded");
        }

        rb2d.velocity = new Vector2(movement * speed, rb2d.velocity.y);      

        if (startJump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.Play("Jump");
            startJump = false;
            isJumping = true;
        }
    }

    void GroundMovement()
    {

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
}
