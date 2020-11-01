using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool isAttacking = false;        //Si está atacando
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
    }

    // Update is called once per frame
    void Update()
    {
        //Si el Game Manager dice que se acabó el juego o está en pausa, salir
        if (GameManager.isGameOver || GameManager.isGamePaused)
        {
            return;
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        //Animación de ataque
        anim.SetTrigger("attack");

        //Detecta los enemigos dentro del rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Spider>().TakeDamage(10);
        }
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

    //Función del evento en la animación de atacar para saber cuando empieza a atacar y cuando termina
    void IsAttacking(int a)
    {
        if (a == 0)
        {
            isAttacking = true;
        }
        else if (a == 1)
        {
            isAttacking = false;
        }
    }

}
