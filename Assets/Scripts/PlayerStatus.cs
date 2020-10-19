using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth = 100;                 //Vida máxima
    private int currentHealth;            //Vida actual

    private bool isHit;                     //Si ha sido golpeado
    private float timeToRecover = 0.5f;     //Tiempo que se queda parado al ser golpeado y que tardará para empezar a moverse de nuevo o atacar
    private float timeRecovered = 0f;       //Tiempo en el que se habrá recuperado
    public bool isRecovered = true;         //Si está recuperado de un golpe recibido para poder moverse

    public bool isDead;                    //Si está muerto

    private Animator anim;

    private PlayerCombat playerCombat;      //Script de combate para cambiar propiedades como el rango de ataque
    private Rigidbody2D rb;

    public enum Equipments { NoWeapon, BaseballBat};
    public Equipments currentEquipment;

    public List<AnimatorOverrideController> AnimControllers = new List<AnimatorOverrideController>();

    private void Start()
    {
        //El equipamiento del jugador
        currentEquipment = Equipments.NoWeapon;

        //Vida del jugador inicial
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //Tiempo que tarda en recuperarse para poder moverse de nuevo o atacar
        if (isHit)
        {
            if (Time.time >= timeRecovered)
            {
                isRecovered = true;
            }
        }
    }

    public void SetEquipment(Equipments newEquipment)
    {
        currentEquipment = newEquipment;

        switch (newEquipment)
        {
            case Equipments.NoWeapon:
                anim.runtimeAnimatorController = AnimControllers[0];
                break;
            case Equipments.BaseballBat:
                anim.runtimeAnimatorController = AnimControllers[1];
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        //Se quita vida
        currentHealth -= damage;

        isHit = true;
        isRecovered = false;
        timeRecovered = Time.time + timeToRecover;

        rb.AddForce(new Vector2(20f, 20f) * -transform.localScale.x);

        //Si la vida es menor igual a 0, muere
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            //Animación de golpeado
            anim.SetTrigger("hit");
        }
    }

    void Die()
    {
        isDead = true;

        //Animación de morir
        anim.SetTrigger("die");
    }
}
