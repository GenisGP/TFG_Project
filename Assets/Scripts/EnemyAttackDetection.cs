using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDetection : MonoBehaviour
{
    public Spider spider;

    private float timeToNextAttack;
    private float timeFirstAttack = 1f;
    private float timeBtwAttack = 2f;
    private bool hasToAttack = false;
    private bool isAttacking = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        //Si se detecta al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus player = collision.GetComponent<PlayerStatus>();
            if (!player.isDead)
            {
                timeToNextAttack = Time.time + timeFirstAttack;
                spider.inCombat = true;
                hasToAttack = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger exit");
        //Si se detecta al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            //timeToAttack = Time.time + timeFirstAttack;
            spider.inCombat = false;
            hasToAttack = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si se detecta al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus player = collision.GetComponent<PlayerStatus>();
            //Si el enemigo está recuperado, puede atacar y el jugador está vivo
            if (!player.isDead)
            {
                if (spider.isRecovered && hasToAttack && Time.time >= timeToNextAttack)
                {
                    spider.Attack();
                    //Se hace daño al jugador
                    player.TakeDamage(10);
                    timeToNextAttack = Time.time + timeBtwAttack;
                }
            }
        }
    }

    private void EndAttack(int a)
    {
        if(a == 0)
        {
            isAttacking = false;
        }else if(a == 1){
            isAttacking = false;
        }

    }
}
