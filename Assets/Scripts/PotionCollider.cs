using System;
using System.Collections;
using UnityEngine;

public class PotionCollider: MonoBehaviour
{
    public PlayerController player;
    public Inventory inv;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.isStrong)
        {
            player.isStrong = true;
            inv.isStrong = true;
            Destroy(this.gameObject);
        }
    }
}