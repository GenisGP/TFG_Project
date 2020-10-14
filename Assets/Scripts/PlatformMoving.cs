using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    public Transform startPos;

    private Vector3 nextPos;
  
    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pointA.position)
        {
            nextPos = pointB.position;
        }
        if (transform.position == pointB.position)
        {
            nextPos = pointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pointA.position, pointB.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //El jugador pasa a ser hijo de la plataforma para moverse con ella
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //El jugador deja a ser hijo de la plataforma
            collision.transform.SetParent(null);
        }
    }
}
