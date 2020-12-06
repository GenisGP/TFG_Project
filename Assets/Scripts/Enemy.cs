using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager player;
    private Transform topEnemy;             //El gameObject que se utiliza para matar al enemigo saltandole encima

    public Transform pointA, pointB;        //Puntos del recorrido
    public float speed;                     //Velocidad
    public int attackDmg;                   //Daño de ataque
    public Transform startPos;              //Primer punto del recorrido al que se dirige
    public int maxHealth;                   //Vida máxima
    private int currentHealth;              //Vida actual

    private bool isRecovered = true;         //Si está recuperado de un golpe recibido para poder moverse
    private bool isDead;                    //Si está muerto
    private bool isAttacking = false;       //Si está en combate contra el jugdor para no moverse

    private float originalXScale;			//Esacala original en el eje X
    private int direction = 1;				//Direccióm a la que mira el personaje (1 -> Derecha, -1-> Izquierda)

    private Vector3 nextPos;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        //Se coge el primer hijo que es el controlador de la parte superior
        topEnemy = this.gameObject.transform.GetChild(0);

        //Se almacena la escala x original del personaje
        originalXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        //Si se llega a un punto, la posición de destino será el otro punto
        if (transform.position == pointA.position)
        {
            nextPos = pointB.position;
            FlipCharacterDirection();
        }
        if (transform.position == pointB.position)
        {
            nextPos = pointA.position;
            FlipCharacterDirection();
        }

        if (isRecovered && !isAttacking)
        {
            //Se mueve el personaje
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

            //Animación de moverse
            anim.SetBool("isMoving", true);
        }

        if (transform.position == pointB.position)
        {
            nextPos = pointA.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pointA.position, pointB.position);
    }

    //Funciones de empezar a atacar y acabar ataque que se llaman des de eventos en la animación de ataque para saber cuando se empieza el ataque y cuando acaba a partir de la animación
    void StartAttack()
    {
        isAttacking = true;
        //Se gira en dirección al personaje si es necesario para atacar
        if(((player.transform.position.x > transform.position.x) && (Mathf.Sign(transform.localScale.x) == 1)) || ((player.transform.position.x < transform.position.x) && (Mathf.Sign(transform.localScale.x) == -1)))
        {
            FlipCharacterDirection();
        }
    }
    void FinishAttack()
    {
        //Se gira en al punto al que se dirige si es necesario después de atacar
        if (((nextPos.x > transform.position.x) && (Mathf.Sign(transform.localScale.x) == 1)) || ((nextPos.x < transform.position.x) && (Mathf.Sign(transform.localScale.x) == -1)))
        {
            FlipCharacterDirection();
        }
        isAttacking = false;
    }

    //Atacar
    public void Attack(int dmg)
    {
        //Si el jugador no está muerto
        if (!player.isDead)
        {
            //Si el jugador no es inmune
            if (!player.isImmune)
            {
                //Animación de atacar
                anim.SetTrigger("attack");
                anim.SetBool("isMoving", false);
                //Se daña al jugador
                player.TakeDamage(dmg);
            }
        }
    }

    //Funciones cuando el enemigo és atacado y se tambalea que se llaman des de eventos en la animación de "Hit" para saber cuando se empieza el tambaleo y cuando termina
    void StartHurt()
    {
        isRecovered = false;
    }
    void EndHurt()
    {
        isRecovered = true;
        //Se gira en al punto al que se dirige si es necesario después de atacar
        if (((nextPos.x > transform.position.x) && (Mathf.Sign(transform.localScale.x) == 1)) || ((nextPos.x < transform.position.x) && (Mathf.Sign(transform.localScale.x) == -1)))
        {
            FlipCharacterDirection();
        }
        isAttacking = false;
    }

    //Recibir daño
    public void TakeDamage(int damage)
    {
        //Se quita vida
        currentHealth -= damage;

        isAttacking = false;
        isRecovered = false;

        //Si la vida es menor igual a 0, muere
        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            //Animación de golpeado
            anim.SetTrigger("hit");
        }
    }

    //Muere
    public void Die()
    {
        isDead = true;

        //Se desactivan colliders
        topEnemy.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        //Animación de morir
        anim.SetTrigger("die");
    }

    //Cuando el enemigo desaparece en la animación de muerte se llama con un evento a la función de desactivar el objeto
    void DeleteObject()
    {
        gameObject.SetActive(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si se detecta al jugador, ataca
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isRecovered)
            {
                Attack(attackDmg);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Si se detecta al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isRecovered)
            {
                Attack(attackDmg);
            }
        }
    }

}