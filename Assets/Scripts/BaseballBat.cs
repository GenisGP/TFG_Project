﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : MonoBehaviour
{
    public PlayerManager player;
    public AudioManager aManager;

    //Al colisionar, si es el jugador y este no tiene el bate equipado, se equipa el bate
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(player.currentEquipment != PlayerManager.Equipments.BaseballBat)
            {
                player.SetEquipment(PlayerManager.Equipments.BaseballBat);

                //Sonido
                aManager.PlaySound("PickBat");

                Destroy(this.gameObject);
            }
        }
    }
}
