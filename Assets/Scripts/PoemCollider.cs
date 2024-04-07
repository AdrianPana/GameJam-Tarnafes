using System;
using System.Collections;
using UnityEngine;

public class PoemCollider: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PoemManager.poemCount++;
            Destroy(this.gameObject);
        }
    }
}