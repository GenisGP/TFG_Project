using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGhost : MonoBehaviour
{
    public SpriteRenderer[] renderers;  //Los sprite renderer de los hijos

    public float timeToStartFade;        //Tiempo que se espera para empezar a desaparecer

    private bool playerCollision;       //Si el jugador ha tocado la plataforma
    private bool isFadingIn;            //Si la plataforma está desapareciendo
    public bool startFadeOut;           //Si la plataforma empieza a parecer
    public bool invisibleAtStart;       //Si tiene que ser invisible al principio

    private AssetAudio assetAudio;

    private void Start()
    {
        assetAudio = GetComponent<AssetAudio>();

        //Si tiene que ser invisible al principio se pone el alpha a 0
        if (invisibleAtStart)
        {
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollision)
        {
            StartCoroutine(FadeIn(timeToStartFade));        
        }
        if (startFadeOut && !isFadingIn)
        {
            //Sonido
            if (assetAudio.GetNameSound() != "Appear")
            {
                assetAudio.PlaySound("Appear");
            }

            StartCoroutine(FadeOut(timeToStartFade));
        }

        //Sonido
        if (isFadingIn && (assetAudio.GetNameSound() != "Disappear"))
        {
            assetAudio.PlaySound("Disappear");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollision = true;
        }
    }

    IEnumerator FadeIn(float time)
    {
        //Empezará después de un tiempo
        yield return new WaitForSeconds(time);

        isFadingIn = true;

        //Para cada sprite renderer de los hijos se baja el alfa periodicamente
        foreach (SpriteRenderer renderer in renderers)
        {
            float alpha = Mathf.Lerp(renderer.color.a, 0f, 2f * Time.deltaTime);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
            
            //Cuando casi no se vea se elimina la plataforma
            if(alpha < 0.2f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator FadeOut(float time)
    {
        //Empezará después de un tiempo
        yield return new WaitForSeconds(time);

        //Para cada sprite renderer de los hijos se baja el alfa periodicamente
        foreach (SpriteRenderer renderer in renderers)
        {
            float alpha = Mathf.Lerp(renderer.color.a, 1f, 2f * Time.deltaTime);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);

            //Cuando se vea la plataforma se activa el collider
            if (alpha > 0.05f)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
