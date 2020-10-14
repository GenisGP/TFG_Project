using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    public Transform startPos;

    private float originalXScale;					//Esacala original en el eje X
    private int direction = 1;						//Direccióm a la que mira el personaje (1 -> Derecha, -1-> Izquierda)

    private Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;

        //Se almacena la escala x original del personaje
        originalXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        if (transform.position == pointB.position)
        {
            nextPos = pointA.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pointA.position, pointB.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
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