using System;
using System.Collections;
using UnityEngine;

public class SwordCollider: MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player") && !player.hasSword)
        {
            player.hasSword = true;
            inv.hasSword = true;
            Destroy(this.gameObject);
        }
    }
}