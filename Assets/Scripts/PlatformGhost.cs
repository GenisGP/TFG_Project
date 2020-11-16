﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGhost : MonoBehaviour
{
    public SpriteRenderer[] renderers;  //Los sprite renderer de los hijos

    public float timeToStartFade;        //Tiempo que se espera para empezar a desaparecer

    private bool playerCollision;       //Si el jugador ha tocado la plataforma
    private bool isFadingIn;            //Si la plataforma está desapareciendo
    public bool startFadeOut;           //Si la plataforma empieza a parecer

    // Update is called once per frame
    void Update()
    {
        if (playerCollision)
        {
            StartCoroutine(FadeIn(timeToStartFade));        
        }
        if (startFadeOut && !isFadingIn)
        {
            StartCoroutine(FadeOut(timeToStartFade));
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

            //Cuando casi no se vea se elimina la plataforma
            if (alpha > 0.8f)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
