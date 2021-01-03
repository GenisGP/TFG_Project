using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 100;            //Vida máxima
    public int currentHealth;              //Vida actual

    public bool isHit;                      //Si ha sido golpeado
    public bool isImmune;                   //Si está en estado de inmunidad despúes de ser golpeado
    private float timeInmune = 2f;        //Tiempo que se queda parado al ser golpeado y que tardará para empezar a moverse de nuevo o atacar
    private float timeEndImmunity = 0f;     //Tiempo en el que dejará de ser inmune

    public bool isDead;                     //Si está muerto

    private Animator anim;

    private PlayerCombat playerCombat;      //Script de combate para cambiar propiedades como el rango de ataque
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    
    public ParticleSystem blood;            //Partículas de sangre

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
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //Tiempo de inmunidad
        if (Time.time >= timeEndImmunity && !GameManager.isGameCompleted)
        {
            isImmune = false;
        }
    }

    public void SetEquipment(Equipments newEquipment)
    {
        currentEquipment = newEquipment;

        switch (newEquipment)
        {
            case Equipments.NoWeapon:
                anim.runtimeAnimatorController = AnimControllers[0];
                playerCombat.attackDmg = 50;
                playerCombat.attackRange = 0.5f;
                break;
            case Equipments.BaseballBat:
                anim.runtimeAnimatorController = AnimControllers[1];
                playerCombat.attackDmg = 100;
                playerCombat.attackRange = 1.5f;
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

        //Partículas
        blood.Play();

        if (currentHealth > 0)
        {
            //Será inmune durante un tiempo
            isImmune = true;

            //Parpadea el personaje
            StartCoroutine(Flash());

            //Sonido
            playerCombat.audioManager.PlaySound("Damage");

            timeEndImmunity = Time.time + timeInmune;
        }


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

        //Sonido
        playerCombat.audioManager.PlaySound("Die");
        
        //Se llama a la función del player manager que indica el fin del juego y enseña el menú
        GameManager.PlayerDied();
    }

    //Para parpadear cuando se recibe daño
    IEnumerator Flash()
    {
        for (int n = 0; n < 8; n++)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
            yield return new WaitForSeconds(0.1f);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
