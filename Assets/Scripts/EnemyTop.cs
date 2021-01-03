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
        if (collision.gameObject.CompareTag("Player"))
        {
            //Para saber si lo ha tocado con los pies
            //Posiciones de los detectores del player de los pies
            Transform playerGroundCheck = enemy.player.transform.Find("GroundCheck");
            Transform playerGroundCheckL = enemy.player.transform.Find("GroundCheckL");
            Transform playerGroundCheckR = enemy.player.transform.Find("GroundCheckR");

            //Si la distancia entre el colisionador del top del enemigo y cualquier detector de los pies del jugador es menor a 1.5, el enemigo muere
            if ((Vector3.Distance(transform.position, playerGroundCheck.position) < 1.5f)||
            (Vector3.Distance(transform.position, playerGroundCheckL.position) < 1.5f) ||
            (Vector3.Distance(transform.position, playerGroundCheckR.position) < 1.5f))
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
}
