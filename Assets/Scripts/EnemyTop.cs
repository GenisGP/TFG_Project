using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTop : MonoBehaviour
{
    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si se detecta al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            //Muere el enemigo
            enemy.Die();

            //Se desactiva este objeto
            gameObject.SetActive(false);

            //Se impulsa al jugador
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }
    }
}
