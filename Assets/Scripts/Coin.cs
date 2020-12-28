using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Coin : MonoBehaviour
{
    public AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySound("Coin");

            GameManager.ScoreIncrease(1);

            Destroy(gameObject);
        }
    }
}
