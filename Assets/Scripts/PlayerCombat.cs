using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerController playerController;

    public bool isAttacking = false;        //Si está atacando
    public bool isRecovered = true;         //Si está recuperado de un golpe recibido para poder moverse

    public int attackDmg;                   //Daño de ataque
    public float attackRate = 2f;           //Cadencia de ataque
    private float nextAttackTime = 0f;

    public Transform attackPoint;           //Punto central del ataque
    public float attackRange;               //Rango de ataque
    public LayerMask enemyLayers;           //Layer de los enemigos para detectarlos al golpearlos

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si el Game Manager dice que se acabó el juego o está en pausa, salir
        if (GameManager.isGameOver || GameManager.isGamePaused)
        {
            return;
        }

        if (Time.time >= nextAttackTime && playerController.isGrounded)
        {
            if (Input.GetMouseButton(0))
            {
                Attack(attackDmg);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    //Funciones cuando el jugador és atacado y se tambalea que se llaman des de eventos en la animación de "Hurt" para saber cuando se empieza el tambaleo y cuando termina
    void StartAttack()
    {
        isAttacking = true;
    }
    void EndAttack()
    {
        isAttacking = false;
    }
    void Attack(int dmg)
    {
        //Animación de ataque
        anim.SetTrigger("attack");

        //Detecta los enemigos dentro del rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Enemy enemyClass = enemy.GetComponent<Enemy>();
            if (enemy)
            {
                enemyClass.TakeDamage(dmg);
            }
        }
    }
    //Funciones cuando el jugador és atacado y se tambalea que se llaman des de eventos en la animación de "Hurt" para saber cuando se empieza el tambaleo y cuando termina
    void StartHurt()
    {
        isRecovered = false;
        isAttacking = false;
    }
    void EndHurt()
    {
        isRecovered = true;
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        //Dibuja una esfera en el editor con centro el punto de ataque y radio el rango de ataque
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
