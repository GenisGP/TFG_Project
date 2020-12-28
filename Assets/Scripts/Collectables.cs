using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum Types { Scroll, Spellbook, Cauldron };
    public Types type;

    public GameMenuUI gameMenuUI;
    public LevelManager levelManager;

    public AudioManager audioManager;

    void Update()
    {
        Debug.Log("cauldron: " + levelManager.items.cauldron);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case Types.Scroll:
                    gameMenuUI.scrollImage.color = new Color(255, 255, 255, 1);
                    levelManager.items.scroll = true;
                    break;
                case Types.Spellbook:
                    gameMenuUI.spellbookImage.color = new Color(255, 255, 255, 1);
                    levelManager.items.spellbook = true;
                    break;
                case Types.Cauldron:
                    gameMenuUI.cauldronImage.color = new Color(255, 255, 255, 1);
                    levelManager.items.cauldron = true;
                    break;
            }

            //Audio
            audioManager.PlaySound("Item");

            Destroy(gameObject);
        }
    }
}
