using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public Transform pointA, pointB;        //Puntos del recorrido
    public float speed;                     //Velocidad
    public Transform startPos;              //Primer punto del recorrido al que se dirige
    public int maxHealth = 50;              //Vida máxima
    private int currentHealth;              //Vida actual

    private bool isHit;                     //Si ha sido golpeado
    private float timeToRecover = 0.5f;     //Tiempo que se queda parado al ser golpeado y que tardará para empezar a moverse de nuevo o atacar
    private float timeRecovered;            //Tiempo en el que se habrá recuperado
    public bool isRecovered = true;        //Si está recuperado de un golpe recibido para poder moverse

    private bool isDead;                    //Si está muerto

    public bool inCombat = false;                  //Si está en combate contra el jugdor para no moverse
    public float attackRate = 1f;           //Cadencia de ataque
    private float nextAttackTime = 0f;
    private bool timeToAttack;              //Si ya ha pasado el tiempo para poder atacar de nuevo

    public Transform attackPoint;           //Punto central del ataque
    public float attackRange;               //Rango de ataque

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

        //Se assigna al attack point el hijo Attack Point
        attackPoint = gameObject.transform.Find("AttackPoint");

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

        if (isRecovered && !inCombat)
        {
            //Se mueve el personaje
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

            //Animación de moverse
            anim.SetBool("isMoving", true);
        }
        else if (inCombat)
        {
            anim.SetBool("inCombat", true);
        }

        if (transform.position == pointB.position)
        {
            nextPos = pointA.position;
        }

        //Tiempo que tarda en recuperarse para poder moverse de nuevo o atacar
        if (isHit)
        {
            if(Time.time >= timeRecovered)
            {
                isRecovered = true;
            }
        }

        //Tiempo que necesita para poder atacar de nuevo
        /*if (Time.time >= nextAttackTime)
        {
            //Puede atacar
            inCombat = false;
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pointA.position, pointB.position);
    }


    public void Attack()
    {
        //Animación de atacar
        anim.SetTrigger("attack");
        anim.SetBool("isMoving", false);

        //Tiempo en el que podrá atacar de nuevo
        nextAttackTime = Time.time + 1f / attackRate;

        //inCombat = true;
    }

    //Recibir daño
    public void TakeDamage(int damage)
    {
        //Se quita vida
        currentHealth -= damage;

        isHit = true;
        isRecovered = false;
        timeRecovered = Time.time + timeToRecover;

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
    void Die()
    {
        isDead = true;

        //Animación de morir
        anim.SetTrigger("die");
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