using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlatformGhost : MonoBehaviour
{
    public BoxCollider2D platform;

    // Update is called once per frame
    void Update()
    {
        //Cuando la plataforma aparece se desactivan las partículas
        if(platform.enabled)
        {
            gameObject.SetActive(false);
        }
    }
}
