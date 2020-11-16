﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaformGhostTriggerFadeOut : MonoBehaviour
{
    private PlatformGhost platform;

    private void Start()
    {
        platform = GetComponentInParent<PlatformGhost>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hola12");
        if (collision.gameObject.CompareTag("Player"))
        {
            platform.startFadeOut = true;
            gameObject.SetActive(false);
            Debug.Log("hola");
        }
    }
}
