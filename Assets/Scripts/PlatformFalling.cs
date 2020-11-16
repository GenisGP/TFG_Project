using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    public PlayerManager player;

    public Transform pointA, pointB;

    public float speedFalling;
    public float speedUpward;
    private float speed;

    public float timeUp;
    public float timeDown;
    public Transform startPos;

    private Vector3 nextPos;

    private bool isMovingUp;
    private bool isMovingDown;

    private BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pointA.position)
        {
            
            StartCoroutine(MoveDown());
        }
        if (transform.position == pointB.position)
        {
            
            StartCoroutine(MoveUp());
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
    IEnumerator MoveUp()
    {
        //Se espera un tiempo para iniciar el desplazamiento
        yield return new WaitForSeconds(timeUp);
        nextPos = pointA.position;
        speed = speedUpward;
        isMovingUp = true;
    }
    IEnumerator MoveDown()
    {
        //Se espera un tiempo para iniciar el desplazamiento
        yield return new WaitForSeconds(timeDown);
        nextPos = pointB.position;
        speed = speedFalling;
        isMovingDown = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isMovingDown)
            {
                //Se lanza un Raycast para saber la distancia respecto el suelo y saber si se está aplastando al personaje
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up);

                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                       //Si la distacncia es menor a la altura del personaje + la mitad de la altura de la plataforma (es decir, se aplasta al personaje contra el suelo)
                       if(hit.distance < 3.7f)
                        {
                            player.TakeDamage(player.currentHealth);
                            //Se desactiva el collider para no empujar al personaje.
                            col.enabled = false;
                        }
                    }
                }


            }
        }
    }
}
