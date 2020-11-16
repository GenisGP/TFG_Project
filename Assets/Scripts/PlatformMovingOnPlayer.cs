using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovingOnPlayer : MonoBehaviour
{
    public Transform destinationPoint;
    public float speed;

    private bool startMoving = false;    //Cuando el jugador esté en la plataforma, esta se moverá

    private Vector3 destinationPointPos;
  
    // Start is called before the first frame update
    void Start()
    {
        destinationPointPos = destinationPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == destinationPointPos)
        {
            return;
        }
        if (startMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPointPos, speed * Time.deltaTime);
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, destinationPoint.position);
    }
    IEnumerator StartMoving()
    {
        //Se espera un tiempo para iniciar el desplazamiento
        yield return new WaitForSeconds(1.5f);
        //Si no se está moviendo, se moverá
        if (!startMoving)
        {
            startMoving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //El jugador pasa a ser hijo de la plataforma para moverse con ella
            collision.transform.SetParent(transform);
            //Lamada a la corrutina de movimiento
            StartCoroutine(StartMoving());
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
