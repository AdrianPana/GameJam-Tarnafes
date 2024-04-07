using System;
using System.Collections;
using UnityEngine;

public class PotionCollider: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerController.isStrong)
        {
            PlayerController.isStrong = true;
            Inventory.isStrong = true;
            Destroy(this.gameObject);
        }
    }
}