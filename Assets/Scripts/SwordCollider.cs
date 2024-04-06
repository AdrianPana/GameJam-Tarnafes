using System;
using System.Collections;
using UnityEngine;

public class SwordCollider: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerController.hasSword)
        {
            PlayerController.hasSword = true;
            Inventory.hasSword = true;
            Destroy(this.gameObject);
        }
    }
}