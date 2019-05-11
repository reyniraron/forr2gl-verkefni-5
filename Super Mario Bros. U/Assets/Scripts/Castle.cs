﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        // Ef leikmaður fer utan í collider á kastala (hægra megin við dyr) hverfur hann
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;  // Hætta að sýna sprite
        }
    }
}