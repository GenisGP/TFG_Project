using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public SpriteRenderer player;
    public ParticleSystem particles;

    private bool isFadingIn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn)
        {
            Debug.Log("is fadding: "+ player.color.a);
           // FadeIn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Se activa el sistema de partículas
            particles.Play();

            //El jugador se desvanece
            //isFadingIn = true;
            player.gameObject.SetActive(false);

            GameManager.PlayerWin();
        }
    }

    void FadeIn()
    {
        float alpha = Mathf.Lerp(player.color.a, 0f, 2f * Time.deltaTime);
        player.color = new Color(player.color.r, player.color.g, player.color.b, alpha);

        //Cuando casi no se vea se elimina el jugador
        if (alpha < 0.05f)
        {
            player.gameObject.SetActive(false);
        }

    }
}
